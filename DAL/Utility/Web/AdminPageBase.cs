using System;
using System.IO;
using System.Web.UI;

namespace com.Utility
{
    /// <summary> 
    /// PageBase ��ժҪ˵����  ҳ������� ����Դ��ά��ҳ�涼�̳�����ҳ��  ��Ҫ��Basic/District.aspx.csΪ�� ����ϸ˵��
    /// ������Դά��ҳ�溯������  �ӿ�  ����  ��ȫһ��   ֻ�Ǿ���������ݸ�ҳ��Ŀؼ��Ĳ�ͬ����ͬ
    /// </summary>
    public class AdminPageBase : Page
    {
        //��ҳ��װ��ʱ������װ�صĺ���  
        //public virtual void PageLoad()
        //{
        //    CheckUserToken();
        //    if (!IsPostBack) //����ǵ�һ�μ���ҳ�� 
        //    {
        //        BindData(); //������
        //    }
        //    else
        //    {
        //        btnAction();
        //    }
        //    BindData();
        //}

        /// <summary>
        /// ����û���ʶ
        /// </summary>
        //protected virtual void CheckUserToken() { }

        ////  ��ҳ������ݣ����ڸ�ҳ�����ܴ󣬹ʱ�����д
        //protected virtual void BindData(){ }
        //protected virtual void btnAction()
        //{
        //    switch (Request["mAction"])
        //    {
        //        case "ActionSel":
        //            btnSel();
        //            break;
        //        case "ActionAdd":
        //            btnAdd();
        //            break;
        //        case "ActionEdit":
        //            btnEdit();
        //            break;
        //        case "ActionDel":
        //            btnDel();
        //            break;
        //        case "ActionAudit":
        //            btnAudit();
        //            break;
        //        case "ActionSave":
        //            btnSave();
        //            break;
        //        case "ActionReturn":
        //            btnReturn();
        //            break;
        //        case "ActionAddChild":
        //            btnAddChild();
        //            break;
        //        case "ActionRestore":
        //            btnRestore();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //protected virtual void btnSel(){}
        //protected virtual void btnAdd(){}
        //protected virtual void btnEdit(){}
        //protected virtual void btnDel(){}
        //protected virtual void btnAudit(){}
        //protected virtual void btnSave(){}
        //protected virtual void btnReturn(){}
        //protected virtual void btnAddChild(){}
        //protected virtual void btnRestore(){}
       
        public virtual string mAction
        {
            get { return ViewState["mAction"].ToString(); }
        }
        public virtual string SortField
        {
            get
            {
                object oSortField = ViewState["gvSortField"];
                if (oSortField == null)
                    return "ID";
                else
                    return oSortField.ToString();
            }
            set { ViewState["gvSortField"] = value; }
        }
        public virtual bool SortType
        {
            get
            {
                object oSortField = ViewState["gvSortType"];
                if (oSortField == null)
                    return true;
                else
                    return (bool) oSortField;
            }
            set { ViewState["gvSortType"] = value; }
        }       
        protected override void SavePageStateToPersistenceMedium(object state)
        {
            Pair pair;
            PageStatePersister persister = this.PageStatePersister;
            object viewState;
            if (state is Pair)
            {
                pair = (Pair) state;
                persister.ControlState = pair.First;
                viewState = pair.Second;
            }
            else
            {
                viewState = state;
            }

            LosFormatter formatter = new LosFormatter();
            StringWriter writer = new StringWriter();
            formatter.Serialize(writer, viewState);
            string viewStateStr = writer.ToString();
            byte[] data = Convert.FromBase64String(viewStateStr);
            byte[] compressedData = zipHelper.Compress(data);
            string str = Convert.ToBase64String(compressedData);

            persister.ViewState = str;
            persister.Save();
        }
        protected override object LoadPageStateFromPersistenceMedium()
        {
            PageStatePersister persister = this.PageStatePersister;
            persister.Load();

            string viewState = persister.ViewState.ToString();
            byte[] data = Convert.FromBase64String(viewState);
            byte[] uncompressedData = zipHelper.DeCompress(data);
            string str = Convert.ToBase64String(uncompressedData);
            LosFormatter formatter = new LosFormatter();
            return new Pair(persister.ControlState, formatter.Deserialize(str));
        }
    }
}