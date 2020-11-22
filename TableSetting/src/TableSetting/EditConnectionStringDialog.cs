using System.Windows.Forms;

namespace TableSetting
{
    public partial class EditConnectionStringDialog : Form
    {
        public EditConnectionStringDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �ݒ���s���L�[���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string Key
        {
            get
            {
                return textKey.Text;
            }
            set
            {
                textKey.Text = value;
            }
        }

        /// <summary>
        /// �ݒ���s���l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public string Value
        {
            get
            {
                return textValue.Text;
            }
            set
            {
                textValue.Text = value;
            }
        }

        /// <summary>
        /// �ݒ肵���L�[�ƒl��L���ɂ��邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool EnableItem
        {
            get
            {
                return checkEnableItem.Checked;
            }
            set
            {
                checkEnableItem.Checked = value;
            }
        }
    }
}