using System;
using System.Collections.Generic;
using System.Text;

namespace com.Model.Base
{
    public enum DataBaseEnum
    {
        Travel = 1
    }

    /// <summary>
    /// ��̨����
    /// </summary>
    public enum App
    {
        Ͷ�����ϵͳ=1
    }
    /// <summary>
    /// ��ǩ����
    /// </summary>
    public enum TagesType
    {
        ������ǩ = 81,
        �򵥱�ǩ = 82,
        ��չ��ǩ = 83
    }
    /// <summary>
    /// ģ�����
    /// </summary>
    public enum TemplateType
    {
        ��ҳ = 41,
        �б�ҳ = 42,
        ����ҳ = 43,
        Ƶ��ҳ = 44
    }
   
    /// <summary>
    /// �û�״̬
    /// </summary>
    public enum UserStatus
    {
        ���� = 31,
        ���� = 32
    }

    /// <summary>
    /// ָʾ��������ӻ�����
    /// </summary>
    public enum AddUpdateType
    {
        Add = 1, // ǿ�����
        Update = 2, // ǿ�Ƹ���
        AddOrUpdate = 3 // �����������, ���������
    }
}
