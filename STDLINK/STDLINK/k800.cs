using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;

#region k800 Class Library in C#
/********************************************************/
//描述：科日新板卡相关(待补全)
//Email: 3206498@sina.com
/********************************************************/
#endregion

namespace STDLINK
{
    /// <summary>
    /// 描述
    /// 
    /// </summary>
    public class k800
    {
        [DllImport("k800.dll")]
        public static extern byte out_port(short nPort, byte cValue);
        [DllImport("k800.dll")]
        public static extern byte in_port(short nPort);

        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceCounter(ref long x);
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceFrequency(ref long x);
    }
    /// <summary>
    /// 描述
    /// 
    /// </summary>
    public class k810A : k800
    { 
        private short address;
        public  k810A(short add)
        {
            address = add;
        }
        public double Read(byte ch,int num)
        {
            double temad;
            short dh, dl;
            dh = 0;
            dl = 0;
            temad = 0;
            for (int i = 0; i < num; i++)
            {
                out_port(address, ch);
                Thread.Sleep(1);
                out_port((short)(address + 0x01), 0);
                Thread.Sleep(5);
                while (in_port(address) < 128) ;
                dh = in_port(address);
                dl = in_port((short)(address + 1));
                temad += (dh & 0xf) * 256 + dl;
            }
            temad  /= num;
            
            return temad;
        }
    }
    /// <summary>
    /// 描述
    /// 
    /// </summary>
    public class k842G : k800
    {
        private short address;
        public k842G(short add)
        {
            address = add;
        }
        public bool In(short ch)
        {
            short cc = (short)((ch) / 8);//计算字节地址
            short dd = (short)((ch) % 8);//计算位地址
            byte tem = 0;
            byte tem1 = 0x01;
            tem = in_port((short)(address + cc));
            tem1 = (byte)(tem1 << dd);
            return (tem & tem1) != 0;
        }
        public byte In2(short ch)
        {
            short cc = (short)((ch) / 8);//计算字节地址
            short dd = (short)((ch) % 8);//计算位地址
            byte tem = 0;
            tem = in_port((short)(address + cc));
            return tem;
        }
    }
    /// <summary>
    /// 描述
    /// 
    /// </summary>
    public class k843 : k800
    {
        private short address;
        private bool[,] k840state = new bool[4, 8];

        public  k843(short add)
        {
            address = add;
        }
        public void CloseAll()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 8; j++)
                    k840state[i, j] = false;
            out_port(address, 0x00);
            out_port((short)(address + 0x01), 0x00);
            out_port((short)(address + 0x02), 0x00);
            out_port((short)(address + 0x03), 0x00);
            Thread.Sleep(2);
            in_port((short)(address ));
        }
        public void Out(short  ch,bool state)
        {
            
            short cc = (short)((ch) / 8);
            short dd = (short)((ch) % 8);
            byte tem = 0;

            k840state[cc, dd] = state;
            for (int i = 0; i < 8; i++)
            {
                if (k840state[cc, i])
                    tem += (byte)Math.Pow(2, i);
            }
            out_port((short)(address + cc), tem);
            Thread.Sleep(2);
            in_port((short)(address ));
        }
    }
    /// <summary>
    /// 描述
    /// 
    /// </summary>
    public class k825 : k800
    {
        private short address;
        private Int32 data = 0;
        private byte dh, dl=0;
        private byte tem;
        public  k825(short add)
        {
            address = add;
        }
        public void k825out(short ch, double value)//ch=0 是第一通道
        {
            //dh = (byte)(value / 256);
            //dl = (byte)(value % 256);
            //out_port(address, dl); //低八位
            //Thread.Sleep(5);
            //out_port((short)(address + 1), dh);//高四位
            //Thread.Sleep(5);
            //tem = in_port((short)(address + ch));

            //i = Val(Text11.Text) * 1000
            data = (Int32)(value / 10 * 4095);

            dh = (byte)(data / 256);
            dl = (byte)(data % 256);
            out_port((short)(address + 1), dh);
            Thread.Sleep(10);
            out_port(address, dl);
            Thread.Sleep(10);
            tem = in_port((short)(address + ch));
        }
    }

    /// <summary>
    /// 描述
    /// 
    /// </summary>
    public class k824 : k800
    {
        private short address;
        private Int32 data = 0;
        private byte dh, dl = 0;
        private byte tem;
        public k824(short add)
        {
            address = add;
        }
        public void k824out(short ch, double value)//ch=0 是第一通道
        {
            data = (Int32)(value / (double)10.0 * (double)4095.0);

            dh = (byte)(data / 256);
            dl = (byte)(data % 256);
           
            out_port(address, dl);
            Thread.Sleep(10);
            out_port((short)(address + 1), dh);
            Thread.Sleep(10);
            tem = in_port((short)(address + ch));
        }
    }

    /// <summary>
    /// 描述
    /// 
    /// </summary>
    public class k840 : k800
    {
        private short address;
        private bool[,] k840state = new bool[4, 8];

        public k840(short add)
        {
            address = add;
        }
        public void CloseAll()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 8; j++)
                    k840state[i, j] = false;
            out_port(address, 0x00);
            out_port((short)(address + 0x01), 0x00);
            out_port((short)(address + 0x02), 0x00);
            out_port((short)(address + 0x03), 0x00);
            Thread.Sleep(2);
            in_port((short)(address));
        }
        public void Out(short ch, bool state)
        {

            short cc = (short)((ch) / 8);
            short dd = (short)((ch) % 8);
            byte tem = 0;

            k840state[cc, dd] = state;
            for (int i = 0; i < 8; i++)
            {
                if (k840state[cc, i])
                    tem += (byte)Math.Pow(2, i);
            }
            out_port((short)(address + cc), tem);
            Thread.Sleep(4);
            in_port((short)(address + 2));

        }

        public bool In(short ch)
        {
            short cc = (short)((ch) / 8);//计算字节地址
            short dd = (short)((ch) % 8);//计算位地址
            byte tem = 0;
            byte tem1 = 0x01;
            tem = in_port((short)(address + cc));
            tem1 = (byte)(tem1 << dd);
            return (tem & tem1) != 0;
        }
    }
    /// <summary>
    /// 描述
    /// 
    /// </summary>
    /// 
    public class k830 : k800
    {
        private short address;

        public k830(short add)
        {
            address = add;
        }
        //out : 1~9 ; type : 工作方式0~5
        public void Initial(int channel, int type)
        {
            BitArray result = new BitArray(8, false);
            //通道
            switch (channel % 3)
            {
                case 1:
                    //通道0
                    break;
                case 2:
                    result[6] = true;
                    //通道1
                    break;
                case 0:
                    //通道2
                    result[7] = true;
                    break;
            }

            //先低后高
            result[5] = true;
            result[4] = true;

            //方式
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    result[1] = true;
                    break;
                case 2:
                    result[2] = true;
                    break;
                case 3:
                    result[1] = true;
                    result[2] = true;
                    break;
                case 4:
                    result[3] = true;
                    break;
                case 5:
                    result[1] = true;
                    result[3] = true;
                    break;
            }
            //编码
            //result[0] =0;
            int nPort2 = address;
            if (channel > 6)
                nPort2 += 11;
            else if (channel > 3)
                nPort2 += 7;
            else
                nPort2 += 3;
            Byte b = 0;
            for (int i = 0; i < 8; i++)
            {
                if (result[i])
                    b |= (Byte)(1 << i);
            }
            out_port((short)nPort2, (Byte)b);

            //Select Case m_ReadWrite
            //    Case 0
            //        End
            //    Case 1
            //        If m_Number > 255 Then
            //            MsgBox "计数器的低八位数据不能大于255,重新设置!", vbInformation + vbOKOnly, "信息提示"
            //            End
            //        End If
            //        m_WriteLow = m_Number
            //        Call out_port(m_BaseAddress + m_AddAddress + m_Count, m_WriteLow)
            //    Case 2
            //        If m_Number > 255 Then
            //            MsgBox "计数器的高八位数据不能大于255,重新设置!", vbInformation + vbOKOnly, "信息提示"
            //            End
            //        End If
            //        m_WriteHight = m_Number
            //        Call out_port(m_BaseAddress + m_AddAddress + m_Count, m_WriteHight)
            //    Case 3
            //        If m_Number > 65535 Then
            //            MsgBox "计数器初值不能大于65535,重新设置!", vbInformation + vbOKOnly, "信息提示"
            //            End
            //        End If
            //       
            //End Select

            //写计数初值
             byte m_WriteHight = 65535 / 256;
             byte m_WriteLow = 65535 % 256;
             out_port(address, m_WriteLow);
             out_port(address, m_WriteHight);
        }
        //channel 1-8
        public void Pulse(int channel, int data)
        {
            int nPort2 = address;
            if (channel > 6)
                nPort2 += channel + 1;
            else if (channel > 3)
                nPort2 += channel;
            else
                nPort2 += channel - 1;
            int dl, dh;
            //dh = (data & 0xf00) >> 8;
            //dl = data & 0xff;
            dh = data / 256;
            dl = data % 256;
            out_port((short)nPort2, (Byte)dl);
            out_port((short)nPort2, (Byte)dh);
        }

        //'m_BaseAddress:板卡基地址;m_Number:计数初值
        //'第九通道分频
        //Public Sub KRX_K830_FREQUENCY(ByVal m_BaseAddress As Integer, ByVal m_Number As Long)
        //    Dim m_ControlNumber As Byte
        //    Dim m_WriteHight As Byte
        //    Dim m_WriteLow As Byte
        //    If m_Number >= 65536 Then
        //        MsgBox "计数初值不能大于65536，重新输入！", vbInformation + vbOKOnly, "信息提示"
        //        End
        //    End If
        //    m_ControlNumber = 180
        //    m_WriteHight = m_Number \ 256
        //    m_WriteLow = m_Number Mod 256
        //    Call out_port(m_BaseAddress + 11, m_ControlNumber)
        //    Call out_port(m_BaseAddress + 10, m_WriteLow)
        //    Call out_port(m_BaseAddress + 10, m_WriteHight)
        //End Sub
        //第九通道分频
        public void FREQUENCY(int data)
        {
            int m_BaseAddress = address;
            byte m_ControlNumber = 180;
            int dl, dh;
            //dh = (data & 0xf00) >> 8;
            //dl = data & 0xff;
            dh = data / 256;
            dl = data % 256;
            m_BaseAddress += 11;
            out_port((short)m_BaseAddress, m_ControlNumber);
            m_BaseAddress -= 1;
            out_port((short)m_BaseAddress, (Byte)dl);
            out_port((short)m_BaseAddress, (Byte)dh);
        }

        //m_BaseAddress:板卡基地址;m_Count:8253计数器选择(0~7);
        //m_ReadWrite:读写计数器的 操作方式(0:计数器封锁;1:读写低8位;2:读写高8位;3:先读写低八位后高八位)
        public int readPulse(int channel, int readwrite)
        {
            int dl, dh;
            int m_BaseAddress = address;
            int m_AddAddress = 0;
            int m_ReadNumber=0;
            switch (channel)
            {
                case 1:
                case 2:
                case 3:
                    m_AddAddress = 0;
                    break;
                case 4:
                case 5:
                case 6:
                    m_AddAddress = 1;
                    break;
                case 7:
                case 8:
                    m_AddAddress = 2;
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("通道选择错误");
                    break;
            }

            switch (readwrite)
            {
                case 0:
                    break;
                case 1:
                    dl = in_port((short)(m_BaseAddress + m_AddAddress + channel - 1));
                    m_ReadNumber = dl;
                    break;
                case 2:
                    dh = in_port((short)(m_BaseAddress + m_AddAddress + channel - 1));
                    m_ReadNumber = dh;
                    break;
                case 3:
                    dl = in_port((short)(m_BaseAddress + m_AddAddress + channel - 1));
                    dh = in_port((short)(m_BaseAddress + m_AddAddress + channel - 1));
                    m_ReadNumber = dh * 256 + dl;
                    break;
                default:
                    break;
            }
            return m_ReadNumber;
        }

        /// <summary>
        /// 描述
        /// 
        /// </summary>
        public class k816 : k800
        {
            private short address;
            public k816(short add)
            {
                address = add;
            }
            public double Read(byte ch, int num)
            {
                double temad;
                short dh, dl;
                dh = 0;
                dl = 0;
                temad = 0;
                for (int i = 0; i < num; i++)
                {
                    out_port(address, ch);
                    Thread.Sleep(1);
                    out_port((short)(address + 0x01), 0);
                    Thread.Sleep(5);
                    while (in_port(address) < 128) ;
                    dh = in_port(address);
                    dl = in_port((short)(address + 1));
                    temad += (dh & 0xf) * 256 + dl;
                }
                temad /= num;

                return temad;
            }
        }
    }
}
