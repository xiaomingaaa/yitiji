﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using yitiji_ma;
namespace yitiji_ma.util
{
    /// <summary>
    /// 使用此工具类创建数据库访问类
    /// </summary>
    class SQLHelper
    {
        private static string sqlconstring = util.ConfigUtil.getConfig().ToString();      
        #region 标准的SQL语句 
        //实现对数据库的增删改
        public static int Update(string sql)
        {
            //实例化一个连接
            SqlConnection conn = new SqlConnection(sqlconstring);
            //实例化Commnad
            SqlCommand cmd = new SqlCommand(sql, conn);
            //执行并返回结果 
            try
            {
                //打开数据库
                conn.Open();
                //返回结果 ,返回值是受影响的行数，int类型
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接 
                cmd.Dispose();
                conn.Close();
            }
        }

        //获得单一结果（第一行第一列）
        public static object GetOneResult(string sql)
        {

            //实例化一个连接
            SqlConnection conn = new SqlConnection(sqlconstring);
            //实例化Commnad
            SqlCommand cmd = new SqlCommand(sql, conn);
            //执行并返回结果 
            try
            {
                //打开数据库
                conn.Open();
                //返回结果 ,返回值是第一行第一列，Object类型
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接 
                conn.Close();
            }
        }

        //获得结果集--SqlDataReader类型
        public static SqlDataReader GetReader(string sql)
        {
            //实例化sqlConnection 
            SqlConnection conn = new SqlConnection(sqlconstring);
            //实例化SqCommand
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                //打开连接 
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //返回结果，返回值SqlDataReader类型
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    //关闭数据库连接 
            //    conn.Close();
            //}
        }
       

        #endregion

        #region 带参数的SQL语句 
        //实现对数据库的增删改
        public static int Update(string sql,SqlParameter[] para)
        {
            //实例化一个连接
            SqlConnection conn = new SqlConnection(sqlconstring);
            //实例化Commnad
            SqlCommand cmd = new SqlCommand(sql, conn);
            //执行并返回结果 
            try
            {
                //打开数据库
                conn.Open();
                //判断参数是否为空
                if(para!=null)
                {
                    cmd.Parameters.AddRange(para);
                }
                //返回结果 ,返回值是受影响的行数，int类型
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接 
                conn.Close();
            }
        }

        //获得单一结果（第一行第一列）
        public static object GetOneResult(string sql,SqlParameter[] para)
        {

            //实例化一个连接
            SqlConnection conn = new SqlConnection(sqlconstring);
            //实例化Commnad
            SqlCommand cmd = new SqlCommand(sql, conn);
            //执行并返回结果 
            try
            {
                //打开数据库
                conn.Open();
                //判断参数是否为空
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                //返回结果 ,返回值是第一行第一列，Object类型
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //关闭数据库连接 
                conn.Close();
            }
        }

        //获得结果集--SqlDataReader类型
        public static SqlDataReader GetReader(string sql,SqlParameter[] para)
        {
            //实例化sqlConnection 
            SqlConnection conn = new SqlConnection(sqlconstring);
            //实例化SqCommand
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                //打开连接 
                conn.Open();
                //判断参数是否为空
                if (para != null)
                {
                    cmd.Parameters.AddRange(para);
                }
                //返回结果，返回值SqlDataReader类型
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SqlDataReader GetReader1(string sql, params SqlParameter[] values)
        {
            SqlConnection con = new SqlConnection(sqlconstring);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddRange(values);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        #endregion

        #region 通过事务提交到数据库
        public static bool UpdateByTransaction(List<string> sqlList)
        {
            //实例化数据库连接
            SqlConnection conn = new SqlConnection(sqlconstring);
            //实例化command类
            SqlCommand cmd = new SqlCommand();
            //指定cmd对象使用的连接对象
            cmd.Connection = conn;
            //执行
            try
            {
                //打开连接
                conn.Open();
                //开启事务
                cmd.Transaction = conn.BeginTransaction();
                //逐条执行SQL
                foreach (string item in sqlList)
                {
                    cmd.CommandText = item;
                    cmd.ExecuteNonQuery();
                }
                //提交事务
                cmd.Transaction.Commit();
                //返回
                return true;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    //回滚事务
                    cmd.Transaction.Rollback();
                }
                throw new Exception("调用事务方法是出现错误！具体信息：" + ex.Message);
            }
            finally
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction = null;
                }
                //关闭连接
                conn.Close();
            }

        }
        #endregion


        //获取数据库服务器的系统时间
        public static DateTime GetServerTime()
        {
            string sql = "Select GETDATE()";

            return Convert.ToDateTime(GetOneResult(sql));
        }
        public static DataTable GetAllResult(string sql)
        {
            using (SqlConnection conn = new SqlConnection(sqlconstring))
            {
                SqlDataAdapter myda = new SqlDataAdapter(sql, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(myda);
                conn.Open();

                DataTable _dt = new DataTable();
                myda.Fill(_dt);
                conn.Close();
                myda.Dispose();
                builder.Dispose();
                return _dt;
            }
        }
    }
}
