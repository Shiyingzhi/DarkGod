using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace MainSever.Tool
{
    public class BaseDate<T>
    {
        public int ID;
    }
    public class AutoGuideCfg : BaseDate<AutoGuideCfg>
    {
        public int npcID;
        public string dilogArr;
        public int actID;
        public int coin;
        public int exp;
    }
    public class ServerXmlCfg
    {
        public ServerXmlCfg()
        {
            InitGuideXml();
        }
        private static ServerXmlCfg _instance;
        public static ServerXmlCfg instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServerXmlCfg();
                }
                return _instance;
            }
        }
        public int AttackCost(int lv, int strength, int intelligence, int physicalPower, int agility)
        {
            int attackNum = lv * 100 + strength * 5 + intelligence * 5 + physicalPower * 3 + agility * 3;
            return attackNum;
        }
        private Dictionary<int, AutoGuideCfg> AutoGuideCfgDic = new Dictionary<int, AutoGuideCfg>();
        #region 解析任务引导xml文件
        public void InitGuideXml()
        {
            
            XmlDocument xml = new XmlDocument();
            xml.Load(@"D:\Documents\DarkGod\Assets\Resources\ResCfg\guide.xml");
            XmlNodeList root = xml.SelectSingleNode("root").ChildNodes;
            for (int i = 0; i < root.Count; i++)
            {
                XmlElement ele = root[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)
                {
                    continue;
                }
                int id = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                AutoGuideCfg guide = new AutoGuideCfg
                {
                    ID = id
                };
                foreach (XmlElement item in root[i].ChildNodes)
                {
                    switch (item.Name)
                    {
                        case "npcID":
                            guide.npcID = int.Parse(item.InnerText);
                            break;
                        case "dilogArr":
                            guide.dilogArr = item.InnerText;
                            break;
                        case "actID":
                            guide.actID = int.Parse(item.InnerText);
                            break;
                        case "coin":
                            guide.coin = int.Parse(item.InnerText);
                            break;
                        case "exp":
                            guide.exp = int.Parse(item.InnerText);
                            break;
                    }
                }
                AutoGuideCfgDic.Add(id, guide);
            }
        }
        #endregion

        public AutoGuideCfg GetAutoGuidCfg(int id)
        {
            AutoGuideCfg guide = null;
            if (!AutoGuideCfgDic.TryGetValue(id, out guide))
            {
                return null;
            }
            return guide;
        }
    }
}
