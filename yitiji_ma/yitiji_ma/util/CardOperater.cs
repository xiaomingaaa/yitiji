﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using yitiji_ma.controller;
using yitiji_ma.entity;
namespace yitiji_ma.util
{
    /// <summary>
    /// 一体机卡操作，所有传输的参数需要
    /// </summary>
    class CardOperater
    {
        /// <summary>
        /// 获取读卡器的串口句柄
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        [DllImport("K720_Dll.dll")]
        public static extern IntPtr K720_CommOpen(string port);
        /// <summary>
        /// 关闭已经打开的句柄
        /// </summary>
        /// <param name="handle">句柄地址</param>
        /// <returns></returns>
        [DllImport("K720_Dll.dll")]
        public static extern int K720_CommClose(IntPtr handle);            
        /// <summary>
        /// 使用发卡指令
        /// K720_SendCmd(ComHandle, MacAddr, “FC7”, 3)	发卡到读卡位置
        /// K720_SendCmd(ComHandle, MacAddr, “FC0”, 3)	发卡到取卡位置
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <param name="macaddr">机器地址（0-15）</param>
        /// <param name="command">发卡指令</param>
        /// <param name="comlength">指令长度</param>
        /// <param name="output">指令执行结果</param>
        /// <returns></returns>
        [DllImport("K720_Dll.dll")]
        public static extern int K720_SendCmd(IntPtr handle, Byte macaddr, string command, int comlength, [MarshalAs(UnmanagedType.LPStr)] StringBuilder output);
        /// <summary>
        /// 读取卡块数据
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <param name="macaddr">机器地址</param>
        /// <param name="sector">扇区号</param>
        /// <param name="block">块号</param>
        /// <param name="outputData">读取内容（16字节）</param>
        /// <param name="record"></param>
        /// <returns></returns>
        [DllImport("K720_Dll.dll")]
        public static extern int K720_S50ReadBlock(IntPtr handle, Byte macaddr, Byte sector, Byte block, Byte[] outputData, [MarshalAs(UnmanagedType.LPStr)] StringBuilder record);
        /// <summary>
        /// 验证卡片扇区密码
        /// 0x30->密码种类A，0x31密码种类B
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="nacaddr"></param>
        /// <param name="sector"></param>
        /// <param name="type"></param>
        /// <param name="blockData"></param>
        /// <param name="recordInfo"></param>
        /// <returns></returns>
        [DllImport("K720_Dll.dll")]
        public static extern int K720_S50LoadSecKey(IntPtr handle, Byte nacaddr, Byte sector, Byte type, byte[] blockData, [MarshalAs(UnmanagedType.LPStr)] StringBuilder recordInfo);
        /// <summary>
        /// 写入块数据
        /// </summary>
        /// <param name="handle">串口句柄</param>
        /// <param name="macAddr">机器地址号</param>
        /// <param name="sector">写入的扇区号</param>
        /// <param name="block">写入的块号</param>
        /// <param name="BlockData">写入块的内容（16字节）</param>
        /// <param name="RecordInfo">记录信息</param>
        /// <returns></returns>
        [DllImport("K720_Dll.dll")]
        public static extern int K720_S50WriteBlock(IntPtr handle, byte macAddr, byte sector, byte block, byte[] BlockData, [MarshalAs(UnmanagedType.LPStr)] StringBuilder RecordInfo);
        /// <summary>
        /// 卡寻卡，判断卡是否真实存在于读卡位置
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="macaddr"></param>
        /// <param name="RecordInfo"></param>
        /// <returns></returns>
        [DllImport("K720_Dll.dll")]
        public static extern int K720_S50DetectCard(IntPtr handle, byte macaddr, [MarshalAs(UnmanagedType.LPStr)] StringBuilder RecordInfo);
        #region
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
        
        byte[] temp = new byte[16];//要写入的16字节存储
        //读出数据的字节数组
        byte[] readByte = new byte[4];
        public long phyid = 0;
        IntPtr handle;
        StringBuilder output;
        byte[] secrect = new byte[6];
        /// <summary>
        /// 构造0扇区2块的数据
        /// </summary>
        /// <param name="mm">学生卡默认初始密码</param>
        /// <param name="lx">卡类型</param>
        /// <returns></returns>
        private byte[] getSecondBlock(string mm, byte lx)
        {
            List<byte> blockData = new List<byte>();
            int m = Convert.ToInt32(mm);
            mm = Convert.ToString(m, 16);
            for (int i = mm.Length; i <= 5; i++)
            {
                mm = "0" + mm;
            }
            //添加最低余额

            blockData.Add(0x00);
            blockData.Add(0x00);
            blockData.Add(0x00);
            blockData.Add(0x00);
            //添加消费密码
            for (int i = 0; i < 3; i++)
            {
                byte temp = Convert.ToByte(mm.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            //添加最大消费额 99999
            blockData.Add(0x00);
            blockData.Add(0x01);
            blockData.Add(0x86);
            blockData.Add(0x9f);
            //限次
            for (int i = 0; i < 4; i++)
            {
                blockData.Add(0xff);
            }
            blockData.Add(lx);
            return blockData.ToArray();
        }
        /// <summary>
        /// 构造0扇区1块的的数据
        /// </summary>
        /// <param name="cardnum">卡号</param>
        /// <param name="name">姓名</param>
        /// <param name="date">过期日期</param>
        /// <returns></returns>
        private byte[] getFristBlock(int cardnum, string name, string date)
        {

            //byte[] numbyte = BitConverter.GetBytes(num);
            string hex = Convert.ToString(cardnum, 16);
            for (int i = hex.Length; i < 5; i++)
            {
                hex = "0" + hex;
            }

            //正常卡的卡号组织
            List<byte> blockData = new List<byte>();
            hex = hex[3] + "" + hex[4] + "" + hex[1] + "" + hex[2] + "8" + hex[0];
            byte[] cardd = new byte[3];
            cardd[0] = 0xd1;
            for (int i = 0; i < 3; i++)
            {
                cardd[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                blockData.Add(cardd[i]);
            }



            byte[] bytename = Encoding.Default.GetBytes(name);


            string[] dd = date.Split('-');
            int year = Convert.ToInt32(dd[0]);
            int month = Convert.ToInt32(dd[1]);
            int day = Convert.ToInt32(dd[2]);
            string hexyear = Convert.ToString(year, 16);
            string hexmonth = Convert.ToString(month, 16);
            string hexday = Convert.ToString(day, 16);

            if (hexday.Length == 1)
            {
                hexday = "0" + hexday;
            }
            date = hexyear + hexmonth + hexday;
            byte[] byteDate = new byte[3];
            blockData.Add(0x28);
            blockData.Add(0x12);
            blockData.Add(0x13);
            //for (int i = 0; i < 3; i++)
            //{
            //    //byteDate[i] = Convert.ToByte(date.Substring(i * 2, 2), 16);
            //    byteDate[i] = 0xff;
            //    blockData.Add(byteDate[i]);
            //}
            blockData.Add(0x00);
            blockData.Add(0x00);
            int length = 8;
            if (bytename.Length <= 8)
            {
                length = bytename.Length;
            }
            for (int i = 0; i < length; i++)
            {
                blockData.Add(bytename[i]);
            }
            int count = blockData.Count();
            for (int i = 16; i > count; i--)
            {
                blockData.Add(0x00);
            }

            return blockData.ToArray();


        }
        /// <summary>
        /// 构造第1扇区第一块的字节数据。
        /// </summary>
        /// <param name="money">金额</param>
        /// <returns></returns>
        private byte[] getSecondSector(double money)
        {
            List<byte> blockData = new List<byte>();
            //金额
            int moneytemp = Convert.ToInt32(money * 100);

            //金额的16进制字符串表示
            string hexmoney = Convert.ToString(moneytemp, 16);



            for (int i = hexmoney.Length; i < 8; i++)
            {
                hexmoney = "0" + hexmoney;
            }
            hexmoney = hexmoney[6] + "" + hexmoney[7] + "" + hexmoney[4] + "" + hexmoney[5] + "" + hexmoney[2] + "" + hexmoney[3] + "" + hexmoney[0] + "" + hexmoney[1];
            Int64 tempMoney = 0;
            for (int i = 0; i < 8; i++)
            {
                Int64 hexValue = getHexValue(hexmoney[i]);
                tempMoney = tempMoney + hexValue * Convert.ToInt32(Math.Pow(16, 7 - i));
            }


            //添加金额 原码
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(hexmoney.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            string hextemp = hexmoney;
            //反码
            tempMoney = ~tempMoney;
            hexmoney = Convert.ToString(tempMoney, 16);
            for (int i = hexmoney.Length; i < 8; i++)
            {
                hexmoney = "F" + hexmoney;
            }
            for (int i = 4; i < 8; i++)
            {
                byte temp = Convert.ToByte(hexmoney.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            //原码
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(hextemp.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            string crc = "04FB04FB";
            //string crc = "05FA05FA";
            //int cardtype = ConfigUtil.getConfig().Cardtype;
            //if (cardtype == 1)
            //{
            //    crc = "04FB04FB";
            //}
            //else
            //{
            //    crc = "05FA05FA";
            //}
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(crc.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            return blockData.ToArray();
        }
        private byte[] getSecondSector2(double money)
        {
            List<byte> blockData = new List<byte>();
            //金额
            int moneytemp = Convert.ToInt32(money * 100);

            //金额的16进制字符串表示
            string hexmoney = Convert.ToString(moneytemp, 16);



            for (int i = hexmoney.Length; i < 8; i++)
            {
                hexmoney = "0" + hexmoney;
            }
            hexmoney = hexmoney[6] + "" + hexmoney[7] + "" + hexmoney[4] + "" + hexmoney[5] + "" + hexmoney[2] + "" + hexmoney[3] + "" + hexmoney[0] + "" + hexmoney[1];
            Int64 tempMoney = 0;
            for (int i = 0; i < 8; i++)
            {
                Int64 hexValue = getHexValue(hexmoney[i]);
                tempMoney = tempMoney + hexValue * Convert.ToInt32(Math.Pow(16, 7 - i));
            }


            //添加金额 原码
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(hexmoney.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            string hextemp = hexmoney;
            //反码
            tempMoney = ~tempMoney;
            hexmoney = Convert.ToString(tempMoney, 16);
            for (int i = hexmoney.Length; i < 8; i++)
            {
                hexmoney = "F" + hexmoney;
            }
            for (int i = 4; i < 8; i++)
            {
                byte temp = Convert.ToByte(hexmoney.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            //原码
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(hextemp.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            string crc = "05FA05FA";
            for (int i = 0; i < 4; i++)
            {
                byte temp = Convert.ToByte(crc.Substring(i * 2, 2), 16);
                blockData.Add(temp);
            }
            return blockData.ToArray();
        }
        /// <summary>
        /// 获取物理卡号
        /// </summary>
        /// <param name="input">读取的0扇区0块的字节数据（4字节）</param>
        /// <returns></returns>
        private long getCardPhyId(byte[] input)
        {
            long temp = 0;
            string hex = "";
            for (int i = 0; i < 4; i++)
            {
                string bytestr = Convert.ToString(input[i], 16);
                if (bytestr.Length == 1)
                {
                    bytestr = "0" + bytestr;
                }
                hex = hex + bytestr;
            }
            hex = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", hex[6], hex[7], hex[4], hex[5], hex[2], hex[3], hex[0], hex[1]);
            for (int i = 0; i < 8; i++)
            {
                Int64 hexValue = getHexValue(hex[i]);
                temp = temp + hexValue * Convert.ToInt64(Math.Pow(16, 7 - i));
            }
            return temp;
        }
        /// <summary>
        /// 根据字符返回16进制数
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Int64 getHexValue(char c)
        {
            Int64 temp = 0;
            switch (c)
            {
                case '0':
                    temp = 0;
                    break;
                case '1':
                    temp = 1;
                    break;
                case '2':
                    temp = 2;
                    break;
                case '3':
                    temp = 3;
                    break;
                case '4':
                    temp = 4;
                    break;
                case '5':
                    temp = 5;
                    break;
                case '6':
                    temp = 6;
                    break;
                case '7':
                    temp = 7;
                    break;
                case '8':
                    temp = 8;
                    break;
                case '9':
                    temp = 9;
                    break;
                case 'a':
                    temp = 10;
                    break;
                case 'b':
                    temp = 11;
                    break;
                case 'c':
                    temp = 12;
                    break;
                case 'd':
                    temp = 13;
                    break;
                case 'e':
                    temp = 14;
                    break;
                case 'f':
                    temp = 15;
                    break;
                default:
                    temp = 0;
                    break;
            }
            return temp;
        }
        public Error WriteCard(string com,int cardnum,string name,string mm,double money,byte lx)
        {
            string studentInfo =string.Format("姓名：{0}，卡号：{1}，卡内余额：{2}",name,cardnum,money);
            try
            {
                if (IsEmptyHandl())
                {
                    handle = K720_CommOpen(com);
                }
                output = new StringBuilder(200);
                int sendcard = K720_SendCmd(handle,0,"FC7",3,output);
                if (sendcard != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("补卡时写卡失败："+studentInfo);
                    return Error.SEND_CARD_ERROR;
                }
                int xunka = K720_S50DetectCard(handle,0,output);
                if (xunka != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("补卡时寻卡失败：" + studentInfo);
                    return Error.DETECT_CARD_ERROR;//寻卡失败
                }
                //验证卡扇区密码数据
                secrect[0] = 0x14;
                secrect[1] = 0x70;
                secrect[2] = 0x25;
                secrect[3] = 0x85;
                secrect[4] = 0x67;
                secrect[5] = 0x58;
                int loadSecret = K720_S50LoadSecKey(handle,0,0,0x30, secrect, output);
                if (loadSecret != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("写卡时0扇区密码验证失败："+studentInfo+","+loadSecret);
                    return Error.PWD_LOAD_ERROR;//密码验证失败
                }
                int readPhy = K720_S50ReadBlock(handle,0,0,0,readByte,output);
                if (readPhy != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("读取新卡物理卡号失败："+studentInfo);
                    return Error.READ_PHYID_ERROR;//读物理卡号失败
                }
                phyid = getCardPhyId(readByte);//获取物理卡号
                string date = DateTime.Now.AddYears(10).ToString("yyyy-MM-dd");
                temp = getFristBlock(cardnum,name,date);
                int writeFristBlock = K720_S50WriteBlock(handle,0,0,1,temp,output);
                if (writeFristBlock != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("卡0扇区块1写入失败："+studentInfo);
                    return Error.WRITE_CARD_ERROR;//写卡失败
                }
                temp = getSecondBlock(mm,lx);
                int writeSecondBlock = K720_S50WriteBlock(handle,0,0,2,temp,output);
                if (writeSecondBlock != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("卡0扇区块2写入失败");
                    return Error.WRITE_CARD_ERROR;
                }
                int loadSecret1 = K720_S50LoadSecKey(handle,0,1,0x30, secrect, output);
                if (loadSecret1 != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("扇区1密码验证失败："+studentInfo);
                    return Error.PWD_LOAD_ERROR;//密码验证失败
                }
                temp = getSecondSector(money);
                string hex = "";
                for (int i = 0; i < temp.Length; i++)
                {
                    hex = hex + Convert.ToString(temp[i],16)+"\t";
                }
                Log.WriteData(hex);
                int writeMoney = K720_S50WriteBlock(handle,0,1,0,temp,output);
                if (writeMoney != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("卡写金额失败："+studentInfo);
                    return Error.WRITE_CARD_ERROR;
                }
                temp = getSecondSector2(money);
                int writeBukaMoney = K720_S50WriteBlock(handle,0,1,1,temp,output);
                if (writeBukaMoney != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("卡写补贴金额失败：" + studentInfo);
                    return Error.WRITE_CARD_ERROR;
                }
                int sendcard1 = K720_SendCmd(handle,0,"FC0",3,output);
                if (sendcard1 != 0)
                {
                    ReleaseHandle(studentInfo);
                    Log.WriteError("发卡到取卡位置失败："+studentInfo);
                    return Error.SEND_CARD_ERROR;//发卡失败
                }
                ReleaseHandle(studentInfo);
                Log.WriteLog("写卡成功："+studentInfo);
                return Error.SEND_CARD_SUCCESS;
            }
            catch (Exception e)
            {
                ReleaseHandle(studentInfo);
                Log.WriteError("写卡失败:"+studentInfo+"\r\n错误信息："+e.Message);
                return Error.WRITE_CARD_ERROR;//写卡失败
            }
        }
        /// <summary>
        /// 关闭已经打开的串口句柄
        /// </summary>
        /// <returns>0成功</returns>
        private void ReleaseHandle(string studentInfo)
        {
            int flag = -1;
            if(!IsEmptyHandl())
                flag= K720_CommClose(handle);
            if (flag != 0)
            {
                Log.WriteError("释放串口句柄失败："+studentInfo);
            }
        }
        private bool IsEmptyHandl()
        {
            return handle == IntPtr.Zero;
        }
    }
}
