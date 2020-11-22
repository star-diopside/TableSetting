using System.Windows.Forms;

namespace TableSetting.Forms
{
    public partial class EditConnectionStringDialog : Form
    {
        public EditConnectionStringDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 設定を行うキーを取得または設定します。
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
        /// 設定を行う値を取得または設定します。
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
        /// 設定したキーと値を有効にするかどうかを示す値を取得または設定します。
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