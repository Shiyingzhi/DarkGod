using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GameWin : UIWinBase {
    public Animation mAnim;
    public Image mRkPiont;
    public Image mRk;
    private bool isOpen = true;

    public Text mPlayerName;
    public Text mRoleLv;
    public Text mExp;
    public Text mTired;
    public Image mProfIcon;
    public Text mAttackNum;
    public Image mExpMask;
    public Image mGuideIcon;
    public Text mPyAndHp;
    public CanvasGroup mCanvasGroup;
    public Image mHp;
    public Image mHpShade;

    public Image AttackBut;
    public Image Skill1;
    public Image Skill2;
    public Image Skill3;
    public Image Skill1Shade;
    public Image Skill2Shade;
    public Image Skill3Shade;

    private Vector2 StartPos = Vector2.zero;
    private AutoGuideCfg CurrentGuideCfg;

    public GameObject mFunction;
    public GameObject mExchange;
    public GameObject mTalkBut;

    public GameObject mCombat;
    private Dictionary<int, string> npcPath = new Dictionary<int, string>();
    /// <summary>
    /// 初始化
    /// </summary>
    protected override void InitWin()
    {
        //初始化任务字典
        InitNpcPath();
        //初始化角色属性
        InitProperty();
        //加载音乐
        AudioSvc.instance.PlayBgClip(GameConstant.BgMainCity, true);
        //隐藏摇杆按钮
        mRkPiont.gameObject.SetActive(false);
        //初始化摇杆
        ControlRocker();
        //初始化技能按钮
        SkillInit();
    }
    //private float EnterTimer = 0;
    private float ComboTimer = 0f; 
    private Vector3 DownScale = new Vector3(0.8f, 0.8f, 0.8f);
    public float Skill1CD = 0;
    public float Skill2CD = 0;
    public float Skill3CD = 0;
    private void SkillInit()
    {
        PEListener PE0 = AttackBut.gameObject.AddComponent<PEListener>();
        PE0.OnClickDown = (PointerEventData eve) => {
            AttackBut.gameObject.transform.localScale = DownScale;
            MainGameSys.instance.AttackBut(true);
            MainGameSys.instance.Attack2But();
            MainGameSys.instance.Attack3But();
            MainGameSys.instance.Attack4But();
            ComboTimer=0.15f;
            
        };
        PE0.OnClickUp = (PointerEventData eve) => {
            AttackBut.gameObject.transform.localScale = Vector3.one;
            //isDown = false; 
            //EnterTimer = 0;

        };
        PEListener PE1 = Skill1.gameObject.AddComponent<PEListener>();
        PE1.OnClickDown = (PointerEventData eve) => { Skill1.gameObject.transform.localScale = DownScale; };
        PE1.OnClickUp = (PointerEventData eve) => 
        {
            Skill1.gameObject.transform.localScale = Vector3.one;
            if (Skill1CD <= 0)
            {
                Skill1CD = 7f;
                MainGameSys.instance.Skill1();
            }
        };
        PEListener PE2 = Skill2.gameObject.AddComponent<PEListener>();
        PE2.OnClickDown = (PointerEventData eve) => { Skill2.gameObject.transform.localScale = DownScale; };
        PE2.OnClickUp = (PointerEventData eve) => 
        {
            Skill2.gameObject.transform.localScale = Vector3.one;
            if (Skill2CD <= 0)
            {
                Skill2CD = 8f;
                MainGameSys.instance.Skill2();
            }
        };
        PEListener PE3 = Skill3.gameObject.AddComponent<PEListener>();
        PE3.OnClickDown = (PointerEventData eve) => { Skill3.gameObject.transform.localScale = DownScale; };
        PE3.OnClickUp = (PointerEventData eve) => 
        {
            Skill3.gameObject.transform.localScale = Vector3.one;
            if (Skill3CD <= 0)
            {
                Skill3CD = 10f;
                MainGameSys.instance.Skill3();
            }
        };
    }


    /// <summary>
    /// 功能切换按钮
    /// </summary>
    public void InitNpcPath()
    {
        npcPath.Add(0, "ResImages/wiseman");
        npcPath.Add(1, "ResImages/general");
        npcPath.Add(2, "ResImages/artisan");
        npcPath.Add(3, "ResImages/trader");
    }
    public void OnExchangeButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (isOpen)
        {
            isOpen = false;
            mAnim.Play("ExchangCloseAnim");
        }
        else
        {
            isOpen = true;
            mAnim.Play("ExchangOpenAnim");
        }
    }
    /// <summary>
    /// 初始化摇杆
    /// </summary>
    public void ControlRocker()
    {
        PEListener PE = mRk.gameObject.AddComponent<PEListener>();
        PE.OnClickDown = (PointerEventData eve) => 
        {
            mRkPiont.gameObject.SetActive(true);
            StartPos = mRkPiont.transform.position;
            mRkPiont.transform.position = eve.position;
        };
        PE.OnClickUp = (PointerEventData eve) =>
        {
            mRkPiont.gameObject.SetActive(false);
            mRkPiont.transform.localPosition = Vector2.zero;
            MainGameSys.instance.SetDir(Vector2.zero);
        };
        PE.OnClickDrag = (PointerEventData eve) =>
        {
            Vector2 dir = eve.position - StartPos;
            float len = dir.magnitude;
            if (len > GameConstant.RockerEange)
            {
                //将image控制在摇杆范围内
                Vector2 clampDir = Vector2.ClampMagnitude(dir, GameConstant.RockerEange); 
                mRkPiont.transform.position = StartPos + clampDir;
            }
            else
            {
                mRkPiont.transform.position = eve.position;
            }
            MainGameSys.instance.SetDir(dir.normalized);
             
        };

    }
    void Update()
    {
        Attack();
    }
    public void InitProperty()
    {
        mPlayerName.text = MainGameSys.instance.mCurrentRole.playerName;
        mRoleLv.text = MainGameSys.instance.mCurrentRole.lv.ToString();
        mExp.text = MainGameSys.instance.mCurrentRole.currentExp .ToString()+ "/" + MainGameSys.instance.mCurrentRole.exp.ToString();
        mTired.text = MainGameSys.instance.mCurrentRole.currentTired +"/" +MainGameSys.instance.mCurrentRole.tired.ToString();
        switch (MainGameSys.instance.mCurrentRole.profession)
        {
            case DarkGodAgreement.Profession.Null:
                break;
            case DarkGodAgreement.Profession.暗影刺客:
                mProfIcon.sprite = ResSvc.intance.LoadSprite(GameConstant.AssassinHead);
                break;
        }
        mAttackNum.text = MainGameSys.instance.mCurrentRole.attackNum.ToString();
        mExpMask.fillAmount = (float)MainGameSys.instance.mCurrentRole.currentExp / (float)MainGameSys.instance.mCurrentRole.exp;
        //设置自动寻路图标
        Debug.Log(MainGameSys.instance.mCurrentRole.guideID);
        CurrentGuideCfg = ResSvc.intance.GetAutoGuidCfg(MainGameSys.instance.mCurrentRole.guideID);
        if (CurrentGuideCfg == null)
        {
            Debug.Log("CurrentGuideCfg为空");
            SetGuideIcon(-1);
        }
        else
        {
            Debug.Log("CurrentGuideCfg不为空");
            SetGuideIcon(CurrentGuideCfg.npcID);
        }
    }
    
    public void SetGuideIcon(int npcID)
    {
        string path = "";
        Debug.Log(npcID);
        if (!npcPath.TryGetValue(npcID, out path))
        {
            //没有任务加载默认图标
            mGuideIcon.sprite = ResSvc.intance.LoadSprite("ResImages/task");
        }
        else 
        {
            mGuideIcon.sprite = ResSvc.intance.LoadSprite(path);
        }
    }
    public void ShowInfo()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        MainGameSys.instance.ShowInfo();
    }

    public void OnCustomsPassClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        MainGameSys.instance.OnCustomsPassClick();
    }
    public void OnGuideClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        if (CurrentGuideCfg != null)
        {
            MainGameSys.instance.RunTask(CurrentGuideCfg);
        }
        else
        {
            GameRoot.ShowHint("更多自动任务，正在开发中敬请期待~~~");
        }
    }

    public void OnIntensifyButCilck()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        MainGameSys.instance.OnIntensifyButCilck();
    }

    public void OnWorldTalkButClick()
    {
        AudioSvc.instance.PlayButClip(GameConstant.ButOnClick);
        MainGameSys.instance.OnWorldTalkButClick();
    }

    
    public void CombatState()
    {
        mFunction.gameObject.SetActive(false);
        mExchange.gameObject.SetActive(false);
        mTalkBut.gameObject.SetActive(false);
        mCombat.gameObject.SetActive(true);
        mPlayerName.rectTransform.localPosition = new Vector3(26, 30, 0);
        mPyAndHp.text = "HP:";
        mTired.text = MainGameSys.instance.mCurrentRole.hp.ToString();

    }

    public void CityState()
    {
        mFunction.gameObject.SetActive(true);
        mExchange.gameObject.SetActive(true);
        mTalkBut.gameObject.SetActive(true);
        mCombat.gameObject.SetActive(false);
        mPlayerName.rectTransform.localPosition = new Vector3(26, 120, 0);
        mPyAndHp.text = "体力:";
        mTired.text = MainGameSys.instance.mCurrentRole.currentTired.ToString();
    }

    public void HideOnOff(int OnOff =1)
    {
        mCanvasGroup.alpha = OnOff;
    }

    public void Attack()
    {
        
        if(CombatSys.instance.isCombat)
        {
            if (ComboTimer > 0)
            {
                ComboTimer -= Time.deltaTime;

            }
            else
            {
                MainGameSys.instance.ResetAttack();
                MainGameSys.instance.AttackBut(false);
            }
            if (Skill1CD > 0)
            {
                Skill1CD -= Time.deltaTime;
                Skill1Shade.fillAmount = Skill1CD / 7f;
            }

            if (Skill2CD > 0)
            {
                Skill2CD -= Time.deltaTime;
                Skill2Shade.fillAmount = Skill2CD / 8f;
            }

            if (Skill3CD > 0)
            {
                Skill3CD -= Time.deltaTime;
                Skill3Shade.fillAmount = Skill3CD / 10f;
            }
        }
        

    }

    public void Skill1Click()
    {
        MainGameSys.instance.Skill1();
    }

    public void Skill2Click()
    {
        MainGameSys.instance.Skill2();
    }

    public void Skill3Click()
    {
        MainGameSys.instance.Skill3();
    }

    public void ShowRoleHp(bool isShow)
    {
        mHp.gameObject.SetActive(isShow);
    }

    public void SetRoleHp(float val,int hp)
    {
        if (hp < 0)
        {
            mTired.text = "0"; mHpShade.fillAmount = 0; return;
        }
        mTired.text = hp.ToString();
        mHpShade.fillAmount = val;
    }
    public void OnTaskClick()
    {
        MainGameSys.instance.OnTaskClick();
    }

    public void CombatStatUpdate()
    {
        mRoleLv.text = MainGameSys.instance.mCurrentRole.lv.ToString();
        mAttackNum.text = MainGameSys.instance.mCurrentRole.attackNum.ToString();
        mExp.text = MainGameSys.instance.mCurrentRole.currentExp.ToString() + "/" + MainGameSys.instance.mCurrentRole.exp.ToString();
        mExpMask.fillAmount = (float)MainGameSys.instance.mCurrentRole.currentExp / (float)MainGameSys.instance.mCurrentRole.exp;
    }

    public void SkillCDInit()
    { 
        Skill1CD = 0;
        Skill2CD = 0;
        Skill3CD = 0;
        Skill1Shade.fillAmount = 0;
        Skill2Shade.fillAmount = 0;
        Skill3Shade.fillAmount = 0;
    }
}
