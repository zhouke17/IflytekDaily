#include "dialog.h"
#include<QMessageBox>

Dialog::Dialog(QWidget *parent)
    : QDialog(parent)
{
    this-> resize(QSize(300,200));
    setWindowTitle(tr("文件对话框"));
    pushButton = new QPushButton(tr("选择"));
    lineEdit = new QLineEdit();
    layOut = new QGridLayout(this);
    layOut -> addWidget(pushButton,0,0);
    layOut -> addWidget(lineEdit,0,1);
    connect(pushButton,SIGNAL(clicked()),this,SLOT(ShowFileName()));

    showDialogBtn = new QPushButton(tr("输入消息对话框"));
    layOut -> addWidget(showDialogBtn,1,0,2,0);
    connect(showDialogBtn,SIGNAL(clicked()),this,SLOT(ShowDialog()));

    msgBtn = new QPushButton(tr("消息对话框"));
    layOut ->addWidget(msgBtn,2,0,2,0);
    connect(msgBtn,SIGNAL(clicked()),this,SLOT(ShowMsgDlg()));

    customBtn = new QPushButton(tr("自定义对话框"));
    layOut -> addWidget(customBtn,3,0);
    label = new QLabel();
    label -> setFrameStyle(QFrame::Panel|QFrame::Sunken);
    layOut -> addWidget(label,3,1);
    connect(customBtn,SIGNAL(clicked()),this,SLOT(ShowCustomDlg()));

}

void Dialog::ShowFileName()
{
    QString str = QFileDialog::getOpenFileName(this,"打开","/","C++ files(*.cpp)::C files(*.c)::Head files(*.h)");
    lineEdit -> setText(str);
}
void Dialog::ShowDialog()
{
    inputDlg = new InputDlg(this);
    inputDlg->show();
}
void Dialog::ShowMsgDlg()
{
    msgDlg = new MsgBoxDlg();
    msgDlg->show();
}
void Dialog::ShowCustomDlg()
{
    //    customDlg = new CustomDlg();
    //    customDlg -> show();
    label -> setText("Custom Box");
    QMessageBox customMsgBox;
    customMsgBox.setIconPixmap(QPixmap("QT.jpeg"));
    customMsgBox.setWindowTitle(tr("自定义消息框"));
    customMsgBox.setText(tr("这是自定义的消息弹窗"));
    QPushButton *yesBtn = customMsgBox.addButton(tr("Yes"),QMessageBox::ActionRole);
    QPushButton *noBtn = customMsgBox.addButton(tr("No"),QMessageBox::ActionRole);
    QPushButton *cancelBtn = customMsgBox.addButton(tr("Cancel"),QMessageBox::ActionRole);
    customMsgBox.exec();
    if(customMsgBox.clickedButton() == yesBtn)
        label -> setText(tr("Custom Message Box/Yes"));
    if(customMsgBox.clickedButton() == noBtn)
        label -> setText(tr("Custom Message Box/No"));
    if(customMsgBox.clickedButton() == cancelBtn)
        label -> setText(tr("Custom Message Box/Cancel"));
    return;
}

Dialog::~Dialog()
{

}
