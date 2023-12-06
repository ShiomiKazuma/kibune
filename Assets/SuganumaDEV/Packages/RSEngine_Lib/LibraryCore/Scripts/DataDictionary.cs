// �Ǘ��� ����
using System;
namespace RSEngine
{
    /// <summary> ����̓o�^���ɑΉ������l���i�[���邽�߂̋@�\��񋟂���N���X�ŁA�������ۂ��@�\��񋟂��� </summary>
    /// <typeparam name="TDataKey"></typeparam>
    /// <typeparam name="TDataValue"></typeparam>
    [Serializable]
    public class DataDictionary<TDataKey, TDataValue>
    {
        //�ėp�f�[�^�y�A
        DataPair<TDataKey, TDataValue>[] _coreDataBase;
        public DataDictionary()//�R���X�g���N�^
        {
            _coreDataBase = new DataPair<TDataKey, TDataValue>[0];//�f�[�^�x�[�X�̃C���X�^���X��
        }
        ~DataDictionary()//�f�X�g���N�^
        {
            _coreDataBase = null;
        }
        public TDataValue this[TDataKey key]
        {
            get { return this.Find(key); }
            set { this.SetAt(key, value); }
        }
        public TDataKey this[TDataValue dataValue]
        {
            get { return this.Find(dataValue); }
            set { this.SetAt(dataValue, value); }
        }
        /// <summary> �f�[�^�̓o�^ </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TDataKey key, TDataValue value)
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Key.Equals(key))
                {
                    throw new Exception("It Seems Already Appended Same Key Value");
                }
            }//Search Same Key Value
            Array.Resize<DataPair<TDataKey, TDataValue>>(ref _coreDataBase, _coreDataBase.Length + 1);
            //Append Element
            _coreDataBase[_coreDataBase.Length - 1] = new DataPair<TDataKey, TDataValue>(key, value);
        }
        /// <summary> �f�[�^�̓o�^���� </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Remove(TDataKey key, TDataValue value)
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Key.Equals(key)//If The Pair Found
                    && _coreDataBase[i].Value.Equals(value))
                {
                    _coreDataBase[i] = null;//Erase Element
                    for (int j = i; j < _coreDataBase.Length; j++)
                    {
                        _coreDataBase[j] = _coreDataBase[(j + 1 < _coreDataBase.Length) ? j + 1 : j];
                        _coreDataBase[(j + 1 < _coreDataBase.Length) ? j + 1 : j] = null;
                        Array.Resize<DataPair<TDataKey, TDataValue>>(ref _coreDataBase, _coreDataBase.Length - 1);
                    }//Make No Gap
                }
            }//Serach Pairs
        }
        /// <summary> Dictionary��Value����Key��Ԃ� </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TDataKey Find(TDataValue value)
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Value.Equals(value))
                {
                    return _coreDataBase[i].Key;
                }
            }
            return default(TDataKey);
        }
        /// <summary> Dictionary��Key����Value��Ԃ� </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TDataValue Find(TDataKey key)
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Key.Equals(key))
                {
                    return _coreDataBase[i].Value;
                }
            }
            return default(TDataValue);
        }
        /// <summary> Value�ɑΉ�����f�[�^��n���ꂽKey�̒l�ɂ��� </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public void SetAt(TDataValue value, TDataKey key)//Set Key By Value
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Value.Equals(value))
                {
                    _coreDataBase[i].SetKey(key);
                    break;
                }
            }
        }
        /// <summary> Key�ɑΉ�����f�[�^��n���ꂽValue�̒l�ɂ��� </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetAt(TDataKey key, TDataValue value)//Set Value By Key
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Key.Equals(key))
                {
                    _coreDataBase[i].SetValue(value);
                    break;
                }
            }
        }
        /// <summary> �f�[�^�y�A��Ԃ� </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataPair<TDataKey, TDataValue> GetDataPair(int index)
        {
            return (_coreDataBase[index] != null) ? _coreDataBase[index] : null;
        }
        /// <summary> ����̃C���f�b�N�X�Ƀf�[�^�������� </summary>
        /// <param name="pair"></param>
        /// <param name="index"></param>
        public void SetDataPair(DataPair<TDataKey, TDataValue> pair, int index)
        {
            _coreDataBase[index] = pair;
        }
    }
    /// <summary> 
    /// Dictionary�݂����ȃf�[�^�y�A�̃N���X�B
    /// List�Ƃ̕��p��z�肵�Đ݌v���Ă���B
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class DataPair<TKey, TValue>
    {
        TKey _key;
        public TKey Key => _key;
        TValue _value;
        public TValue Value => _value;
        /// <summary> ������^��Key�̒l </summary>
        public string SKey => _key.ToString();
        /// <summary> ������^��Value�̒l </summary>
        public string SValue => _value.ToString();
        public DataPair(TKey key, TValue value)//�R���X�g���N�^
        {
            _key = key;
            _value = value;
        }
        /// <summary> Key���Z�b�g </summary>
        /// <param name="key"></param>
        public void SetKey(TKey key)
        {
            this._key = key;
        }
        /// <summary> Value���Z�b�g </summary>
        public void SetValue(TValue value)
        {
            this._value = value;
        }
        /// <summary> Key�̌^��Ԃ� </summary>
        public Type KeyType => _key.GetType();
        /// <summary> Value�̌^��Ԃ� </summary>
        public Type ValueType => _value.GetType();
    }
}