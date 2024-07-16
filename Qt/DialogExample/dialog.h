#ifndef DIALOG_H
#define DIALOG_H

#include <QFileDialog>
#include <QPushButton>
#include <QGridLayout>
#include <QLineEdit>
#include "inputdlg.h"
#include "msgboxdlg.h"
#include "customdlg.h"

class Dialog : public QDialog
{
    Q_OBJECT
public:
    Dialog(QWidget *parent = 0);
    ~Dialog();
private:
    QPushButton *pushButton;
    QLineEdit *lineEdit;
    QGridLayout *layOut;

    InputDlg *inputDlg;//声明标准输入对话框类实例
    QPushButton *showDialogBtn;

    QPushButton *msgBtn;
    MsgBoxDlg *msgDlg;

    QPushButton *customBtn;
    QLabel *label;
    CustomDlg *customDlg;

private slots:
    void ShowFileName();
    void ShowDialog();//弹出输入对话框的槽函数
    void ShowMsgDlg();//弹出消息对话框槽函数
    void ShowCustomDlg();//弹出自定义对话框槽函数
};

#endif // DIALOG_H
