#include "msgboxdlg.h"
#include <QMessageBox>

MsgBoxDlg::MsgBoxDlg(QWidget *parent)
    : QDialog (parent)
{
       this-> resize(QSize(300,200));//设置窗体大小
       setWindowTitle(tr("标准消息对话框实例"));//设置对话框的标题
       label = new QLabel;
       label -> setText(tr("请选择一种消息框"));
       questionBtn = new QPushButton;
       questionBtn -> setText(tr("QuestionMsg"));
       informationBtn =new QPushButton;
       informationBtn->setText(tr("InformationMsg"));
       warningBtn =new QPushButton;
       warningBtn->setText(tr("WarningMsg"));
       criticalBtn = new QPushButton;
       criticalBtn->setText(tr("CriticalMsg"));
       aboutBtn=new QPushButton;
       aboutBtn->setText(tr("AboutMsg"));
       aboutQtBtn =new QPushButton;
       aboutQtBtn->setText(tr("AboutQtMsg"));
       //布局
       mainLayout = new QGridLayout(this);
       mainLayout->addWidget(label,0,0);
       mainLayout->addWidget (questionBtn,1,0);
       mainLayout->addWidget(informationBtn,1,1);
       mainLayout->addWidget(warningBtn,2,0);
       mainLayout->addWidget(criticalBtn,2,1);
       mainLayout->addWidget(aboutBtn,3,0);
       mainLayout->addWidget(aboutQtBtn,3,1);
       mainLayout->setSpacing(15);
       //事件关联
       connect(questionBtn,SIGNAL(clicked()),this,SLOT(showQuestionMsg()));
       connect(informationBtn,SIGNAL(clicked()),this,SLOT(showInformationMsg()));
       connect(warningBtn,SIGNAL(clicked()),this,SLOT(showWarningMsg()));
       connect(criticalBtn,SIGNAL(clicked()),this,SLOT(showCriticalMsg()));
       connect(aboutBtn,SIGNAL(clicked()),this,SLOT(showAboutMsg()));
       connect(aboutQtBtn,SIGNAL(clicked()),this,SLOT(showAboutQtMsg()));
}
void MsgBoxDlg::showQuestionMsg()
{
    label -> setText(tr("Question message box"));
    switch(QMessageBox::question(this,tr("Question 消息框"),tr("是否退出程序？"),QMessageBox::Ok|QMessageBox::Cancel,QMessageBox::Ok))
    {
    case QMessageBox::Ok:
        label -> setText(tr("MessageBox button/ok"));
        break;
    case QMessageBox::Cancel:
        label -> setText(tr("MessageBox button/cancel"));
        break;
    default:
        break;
    }
    return;
}
void MsgBoxDlg::showInformationMsg()
{
    label -> setText(tr("Information message box"));
    QMessageBox::information(this,tr("Information 消息框"),tr("Information 你好！"));
    return;
}
void MsgBoxDlg::showWarningMsg()
{
    label -> setText(tr("Warning message box"));
    switch(QMessageBox::warning(this,tr("Warning 消息框"),tr("修改的内容是否保存？"),QMessageBox::Save|QMessageBox::Discard|QMessageBox::Cancel,QMessageBox::Save))
    {
    case QMessageBox::Save:
        label -> setText(tr("Warning button/save"));
        break;
    case QMessageBox::Discard:
        label -> setText(tr("Warning button/discard"));
        break;
    case QMessageBox::Cancel:
        label -> setText(tr("Warning button/cancel"));
        break;
    default:
        break;
    }
    return;
}
void MsgBoxDlg::showCriticalMsg()
{
    label -> setText(tr("Critical message box"));
    QMessageBox::critical(this,tr("Critical 消息框"),tr("这是Critical 消息框"));
    return;
}
void MsgBoxDlg::showAboutMsg()
{
    label -> setText(tr("About message box"));
    QMessageBox::about(this,tr("About 消息框"),tr("这是About 消息框"));
    return;
}
void MsgBoxDlg::showAboutQtMsg()
{
    label -> setText(tr("AboutQt message box"));
    QMessageBox::aboutQt(this,tr("这是AboutQt 消息框"));
    return;
}
MsgBoxDlg::~MsgBoxDlg()
{

}
