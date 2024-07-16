#include "layout.h"

Layout::Layout(QWidget *parent)
    : QDialog(parent)
{
    setWindowTitle(tr("编辑用户信息"));
    //左侧
    UserNameLabel = new QLabel(tr("用户名:"));
    UserNameLineEdit = new QLineEdit;
    Namelabel = new QLabel(tr("姓名:"));
    NameLineEdit =new QLineEdit;
    SexLabel = new QLabel(tr("性别:"));
    SexComboBox = new QComboBox;
    SexComboBox->addItem(tr("女"));
    SexComboBox->addItem(tr("男"));
    DepartmentLabel = new QLabel(tr("部门:"));
    DepartmentTextEdit = new QTextEdit;
    AgeLabel =new QLabel(tr("年龄:"));
    AgeLineEdit =new QLineEdit;
    OtherLabel =new QLabel(tr("备注:"));
    OtherLabel-> setFrameStyle(QFrame::Panel|QFrame::Sunken);//设置控件风格(形状：NoFrame、Panel、Box、HLine、VLine及WinPanel|阴影：Plain、Raised和Sunken)
    LeftLayout = new QGridLayout();//向布局中加入需要布局的控件
    LeftLayout->addWidget(UserNameLabel,0,0);//用户名
    LeftLayout->addWidget(UserNameLineEdit,0,1);
    LeftLayout->addWidget(Namelabel,1,0);//姓名
    LeftLayout->addWidget(NameLineEdit,1,1);
    LeftLayout->addWidget(SexLabel,2,0);//性别
    LeftLayout->addWidget(SexComboBox,2,1);
    LeftLayout->addWidget(DepartmentLabel,3,0);//部门
    LeftLayout->addWidget(DepartmentTextEdit,3,1);
    LeftLayout->addWidget (AgeLabel,4,0);//年龄
    LeftLayout->addWidget(AgeLineEdit,4,1);
    LeftLayout->addWidget(OtherLabel,5,0,1,2);//其他,第五行，第一列，占用一行、两列的宽度
    LeftLayout->setColumnStretch(0,1);//第一列
    LeftLayout->setColumnStretch(1,3);//第二列，设置左右两列宽度比为1：3

    /*  右侧  */
    //右上角部分
    HeadLabel = new QLabel(tr("头像: "));
    HeadIconLabel =new QLabel;
    QPixmap icon("head.jpeg");
    //    QPixmap icon(":/img/Images/head.jpeg");//设置不成功？？？
    //调整图片大小方法一
    //    HeadIconLabel -> setMaximumSize(25,25);
    //    HeadIconLabel -> setScaledContents(true);
    //调整图片大小方法二
    icon = icon.scaled(25, 25, Qt::KeepAspectRatio);
    HeadIconLabel -> setPixmap(icon);
    UpdateHeadBtn = new QPushButton(tr("更新"));

    //完成右上侧头像选择区的布局
    TopRightLayout =new QHBoxLayout();
    TopRightLayout->setSpacing(10);    //设定各个控件之间的间距为 20
    TopRightLayout->addWidget(HeadLabel);
    TopRightLayout->addWidget(HeadIconLabel);
    TopRightLayout->addStretch();//添加占位符，使头像紧挨着左侧
    TopRightLayout->addWidget(UpdateHeadBtn);
    TopRightLayout -> setContentsMargins(0,20,0,0);//设置内容外边距

    //右下角部分
    IntroductionLabel = new QLabel(tr("个人说明:"));
    IntroductionTextEdit = new QTextEdit;

    //完成右侧的布局
    RightLayout =new QVBoxLayout();
    RightLayout->setMargin(10);
    RightLayout->addLayout(TopRightLayout);
    RightLayout->addWidget (IntroductionLabel);
    RightLayout->addWidget(IntroductionTextEdit);

    /*  底部  */
    OkBtn = new QPushButton(tr("确定"));
    CancelBtn = new QPushButton(tr("取消"));
    ButtomLayout = new QHBoxLayout();
    ButtomLayout -> addStretch();//插入占位符,使两个按钮能够靠右对齐
    ButtomLayout -> addWidget(OkBtn);
    ButtomLayout -> addWidget(CancelBtn);
    ButtomLayout -> setMargin(10);

    QGridLayout *mainLayout = new QGridLayout(this);
    mainLayout -> setMargin(15);
    mainLayout -> setSpacing(10);
    mainLayout -> addLayout(LeftLayout,0,0);
    mainLayout -> addLayout(RightLayout,0,1);
    mainLayout -> addLayout(ButtomLayout,1,0,1,2);
    mainLayout ->setSizeConstraint(QLayout::SetFixedSize);// 设定最优化显示，并且使用户无法改变对话框的大小。

    connect(OkBtn, &QPushButton::clicked, this, [=](){
        this->close();
    });
    connect(CancelBtn,&QPushButton::clicked,this,[=](){ this -> close();});
}

Layout::~Layout()
{

}
