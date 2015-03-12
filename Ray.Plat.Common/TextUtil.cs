using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml;

namespace Ray.Plat.Common
{
    public class TextUtil
    {
        #region 基本字符串操作

        /// <summary>
        /// 返回DateTime类型
        /// </summary>
        /// <param name="input">输入的字符串</param>
        public static DateTime GetDateTime(string input)
        {
            if (input == null || input == "")
            {
                return new DateTime(1900, 1, 1);
            }
            else
            {
                try
                {
                    return DateTime.Parse(input);
                }
                catch
                {
                    return new DateTime(1900, 1, 1);
                }
            }
        }

        /// <summary>
        /// 获取字符串，若为null则返回“”
        /// </summary>
        public static string GetString(string input)
        {
            if (input == null)
            {
                return "";
            }
            else
            {
                return input;
            }
        }

        public static float GetFloat(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            else
            {
                return float.Parse(input);
            }
        }

        public static decimal GetDecimal(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            else
            {
                return decimal.Parse(input);
            }
        }
        /// <summary>
        /// 将字符串转换为bool类型（1：true，0：false）
        /// </summary>
        public static bool GetBool(string input)
        {
            if (!string.IsNullOrEmpty(input) && input=="1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 截取字符串的前cnt个字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="cnt">截取的字符数量</param>
        /// <returns></returns>
        public static string PreString(string str,int cnt)
        {
            if (str.Length <= cnt)
            {
                return str;
            }
            else
            {
                return str.Substring(0, cnt) + "...";
            }
        }

        /// <summary>
        /// 拆分字符串,默认分隔符为','
        /// </summary>
        /// <param name="str">待拆分的字符串</param>
        /// <param name="speater">分隔符</param>
        /// <returns>返回拆分后的字符串数组</returns>
        public static string[] GetStrArray(string str,char speater)
        {
            return str.Split(speater);
        }

        /// <summary>
        /// 将集合中的项目合并为一个字符串,并用指定的字符分割
        /// </summary>
        /// <param name="list">待合并的字符串集合</param>
        /// <param name="speater">分隔符</param>
        /// <returns>返回合并后的字符串</returns>
        public static string GetArrayStr(List<string> list,string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="strchar">要删除的字符</param>
        public static string DelLastChar(string str,string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion

        #region 把汉字转化成全拼音
        private static int[] pyValue = new int[]
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
            -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
            -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
            -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
            -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
            -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
            -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
            -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
            -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
            -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
            -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
            -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
            -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
            -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
            -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
            -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
            -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
            -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
            -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
            -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
            -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
            -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
            -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
        };

        private static string[] pyName = new string[]
        {
        "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
        "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
        "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
        "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
        "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
        "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
        "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
        "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
        "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
        "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
        "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
        "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
        "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
        "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
        "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
        "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
        "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
        "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
        "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
        "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
        "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
        "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
        "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
        "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
        "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
        "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
        "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
        "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
        "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
        "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
        "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
        "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
        "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };

        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string ConvertE(string hzString)
        {
            // 匹配中文字符
            Regex regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = hzString.ToCharArray();

            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        // 修正部分文字
                        if (chrAsc == -9254)  // 修正“圳”字
                            pyString += "Zhen";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    pyString += pyName[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                // 非中文字符
                else
                {
                    pyString += noWChar[j].ToString();
                }
            }
            return pyString;
        }
        #endregion

        #region 获取xheditor中的图片集合
        public static string GetHtmlImageUrlList(string sHtmlText)
        {
            try
            {
                // 定义正则表达式用来匹配 img 标签  
                Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

                // 搜索匹配的字符串  
                MatchCollection matches = regImg.Matches(sHtmlText);

                //int i = 0;
                if (matches.Count > 0)
                {
                    return matches[0].Groups["imgUrl"].Value;
                }
                else return "images/nopic.jpg";
            }
            catch
            {
                return "images/nopic.jpg";
            }
        }  

        #endregion

        #region 把条件变成:(1,2,3)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="name">数据项</param>
        /// <returns></returns>
        public static string GetCondition(DataTable dt,string name)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("(");
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (count > 0)
                {
                    condition.Append(",'" + row[name].ToString() + "'");
                    count++;
                }
                else
                {
                    condition.Append("'" + row[name].ToString() + "'");
                    count++;
                }
            }
            condition.Append(")");
            return condition.ToString();
        }

        /// <summary>
        /// 获取条件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetCondition(string str,char s)
        {
            StringBuilder result = new StringBuilder();
            string[] temp = str.Split(s);
            for (int i = 0; i < temp.Length - 1; i++)
            {
                if (i > 0)
                {
                    result.Append(",'" + temp[i] + "'");
                }
                else
                {
                    result.Append("'" + temp[i] + "'");
                }
            }
            return result.ToString();
        }

        public static string GetCondition(List<string> list)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    result.Append("+'");
                    result.Append(list[i]);
                    result.Append("'");
                }
                else
                {
                    result.Append("'"+list[i]+"'");
                }
            }
            return result.ToString();
        }
        #endregion

        #region xml操作
        public static XmlDocument GetXMLDoc(string path)
        {
            if (path == string.Empty)
            {
                return null;
            }

            XmlDocument doc = null;
            System.Xml.XmlTextReader xmlReader = null;
            try
            {
                xmlReader = new XmlTextReader(path);
                xmlReader.WhitespaceHandling = WhitespaceHandling.None;
                doc = new XmlDataDocument();
                doc.Load(xmlReader);

                xmlReader.Close();

                return doc;
            }
            catch (System.Xml.XmlException xe)
            {
                throw xe;
            }
            catch (System.Exception ee)
            {
                throw ee;
            }
        }
        #endregion

        #region Base64解码转码操作

        /// <summary>
        /// 编码base64
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns></returns>
        public static string EncodeBase64(string para)
        {
            byte[] bytes = Encoding.Default.GetBytes(para);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 解码base64
        /// </summary>
        /// <param name="para">参数</param>
        /// <returns></returns>
        public static string DecodeBase64(string para)
        {
            byte[] bytes = Convert.FromBase64String(para);
            return Encoding.Default.GetString(bytes);
        }
        #endregion

        #region 根据总数计算页数
        public static int GetPages(int count,int rows)
        {
            int pages = 0;
            int flag = count % rows;
            if (flag == 0)
            {
                pages = count / rows;
            }
            else
            {
                pages = (count / rows) + 1;
            }
            return pages;
        }
        #endregion
    }
}
