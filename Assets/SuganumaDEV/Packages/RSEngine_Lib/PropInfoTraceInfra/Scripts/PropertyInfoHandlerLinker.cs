// �Ǘ��� ����
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary> �v���p�e�B���n���h���[�ǂ����̃����J�̋@�\��񋟂��� </summary>
public class PropertyInfoHandlerLinker : MonoBehaviour
{
    /// <summary> �v���p�e�B�Q�ƌ��̏��n���h���[ </summary>
    [SerializeField] PropertyInfoHandler _sender; // �v���p�e�B�Q�ƌ�
    public PropertyInfoHandler Sender => _sender;
    /// <summary> �v���p�e�B�Q�ƌ��̃f�[�^�̏�������i�^�[�Q�b�g�j </summary>
    [SerializeField] PropertyInfoHandler _receiver; // �v���p�e�B�Q�ƒl�̏�������
    public PropertyInfoHandler Receiver => _receiver;
    List<string> _senderResisters = new();
    public List<string> SenderResisters => _senderResisters;
    List<string> _receiverResisters = new();
    public List<string> ReceiverResisters => _receiverResisters;
    public event Action OnSenderDataUpdated;
    public event Action OnSenderDataSendedToReceiver;
    public event Action OnReceiverDataUpdated;
    public event Action OnReceiverrDataSendedToSender;
    private void Start()
    {
        if (_sender == null)
        { Debug.LogWarning("You Need Assing Sender PropInfoHandler"); }
        if (_sender == null)
        { Debug.LogWarning("You Need Assing Receiver PropInfoHandler"); }
        if (_sender == null && _receiver == null)
        { throw new Exception("You Need Assing Both Sender And Receiver"); }
    }
    #region ���ʕ�
    /// <summary> �f�[�^�̉��������T�|�[�g���郁�\�b�hSender����Receiver�֗��� </summary>
    /// <param name="senderPropHandler"></param>
    /// <param name="senderReristerName"></param>
    /// <param name="receiverPropHandler"></param>
    /// <param name="receiverResisterName"></param>
    void PassData(PropertyInfoHandler senderPropHandler, string senderReristerName, PropertyInfoHandler receiverPropHandler, string receiverResisterName)
    {
        receiverPropHandler.DataMap[receiverResisterName] = senderPropHandler.DataMap[senderReristerName];
    }
    /// <summary> �o�^�����l�̍X�V </summary>
    /// <param name="resisterName"></param>
    /// <param name="value"></param>
    void UpdateData(PropertyInfoHandler propHandler, string resisterName, object value) // �v���p�e�B�Q�ƌ��ɓo�^����Ă���o�^���ɑΉ������l�̍X�V
    {
        propHandler.DataMap[resisterName] = value;
    }
    /// <summary> �f�[�^�x�[�X����̃f�[�^�̒��o </summary>
    /// <param name="resisterIndex"></param>
    /// <returns></returns>
    object ExtractData(PropertyInfoHandler propHandler, string resisterName)
    {
        return propHandler.DataMap[resisterName];
    }
    #endregion
    #region �v���p�e�B���Z���_
    /// <summary> �Z���_�[�o�^�����X�g�̓o�^ </summary>
    /// <param name="resisterNames"></param>
    public void ApplySenderResisterList(List<string> resisterNames) // �Q�ƌ�����Ăяo�����
    {
        _senderResisters = resisterNames;
    }
    /// <summary> �����J�Ɏw�肳�ꂽ���V�[�o�[�̃f�[�^���Z���_�[�ɓo�^����Ă�l�ɂ��� </summary>
    /// <param name="senderResisterName"> �����J�Ɏw�肳��Ă���Z���_�[�̕ێ����郌�W�X�^�� </param>
    /// <param name="receiverResisterName"> �����J�Ɏw�肳��Ă郌�V�[�o�[�̕ێ����郌�W�X�^�� </param>
    public void SendDataSenderToReceiver(string senderResisterName, string receiverResisterName)
    {
        PassData(_sender, senderResisterName, _receiver, receiverResisterName);
        if (OnSenderDataSendedToReceiver != null) { OnSenderDataSendedToReceiver(); }
    }
    /// <summary> �����J�Ɏw�肳��Ă���Z���_�[�̃��W�X�^�̃f�[�^���X�V���� </summary>
    /// <param name="resisterName"> �Z���_�[�̃��W�X�^�� </param>
    /// <param name="value"> �X�V��̒l </param>
    public void UpdateSenderData(string resisterName, object value)
    {
        UpdateData(_sender, resisterName, value);
        if (OnSenderDataUpdated != null) { OnSenderDataUpdated(); }
    }
    /// <summary> �����J�Ɏw�肳��Ă���Z���_�[����l�𒊏o���� </summary>
    /// <param name="resisterName"> �Z���_�[���̃��W�X�^�� </param>
    /// <returns></returns>
    /// <exception cref="Exception"> �f�[�^��������Ȃ��ꍇ�ɓ������� </exception>
    public object ExtractDataFromSender(string resisterName)
    {
        var ret = ExtractData(_sender, resisterName);
        return (ret != null) ? ret : throw new Exception("Data Wasnt Found");
    }
    #endregion
    #region �v���p�e�B��񃌃V�[�o
    /// <summary> ���V�[�o�[�o�^���̃��X�g�̓o�^ </summary>
    /// <param name="resisterNames"></param>
    public void ApplyReceiverResisterList(List<string> resisterNames) // �Q�ƌ�����Ăяo�����
    {
        _receiverResisters = resisterNames;
    }
    /// <summary> �����J�Ɏw�肳�ꂽ�Z���_�[�̃��W�X�^�̓o�^�l�����V�[�o�[�̃��W�X�^�ɓo�^����Ă�l�ɂ��� </summary>
    /// <param name="receiverResisterName"> �����J�Ɏw�肳��Ă郌�V�[�o�[�̕ێ����郌�W�X�^�� </param>
    /// <param name="senderResisterName"> �����J�Ɏw�肳��Ă���Z���_�[�̕ێ����郌�W�X�^�� </param>
    public void SendDataReceiverToSender(string receiverResisterName, string senderResisterName)
    {
        PassData(_receiver, receiverResisterName, _sender, senderResisterName);
        if (OnReceiverrDataSendedToSender != null) { OnReceiverrDataSendedToSender(); }
    }
    /// <summary> �����J�Ɏw�肳��Ă��郌�V�[�o�[�̃f�[�^�̍X�V </summary>
    /// <param name="resisterName"> �����J�Ɏw�肳��Ă��郌�V�[�o�[�̃��W�X�^�� </param>
    /// <param name="value"> �X�V��̒l </param>
    public void UpdateReceiverData(string resisterName, object value)
    {
        UpdateData(_receiver, resisterName, value);
        if (OnReceiverDataUpdated != null) { OnReceiverDataUpdated(); }
    }
    /// <summary> �����J�Ɏw�肳��Ă��郌�V�[�o�[����f�[�^�̒��o������ </summary>
    /// <param name="resisterName"> ���V�[�o�[�̃��W�X�^�� </param>
    /// <returns></returns>
    /// <exception cref="Exception"> �f�[�^��������Ȃ��ꍇ�ɓ������� </exception>
    public object ExtractDataFromReceiver(string resisterName)
    {
        var ret = ExtractData(_receiver, resisterName);
        return (ret != null) ? ret : throw new Exception("Data Wasnt Found");
    }
    #endregion
}