#ifndef DIALOG_H
#define DIALOG_H

#include <QDialog>
#include <QLabel>
#include <QLineEdit>
#include <QPushButton>

namespace Ui {
class Dialog;
}

class Dialog : public QDialog
{
    Q_OBJECT

public :
    Dialog(QWidget *parent = nullptr);
    ~Dialog();
private:
    Ui::Dialog *ui;
    QLabel *label1,*label2;
    QLineEdit *lineEdit;
    QPushButton *pushButton;
};

#endif // DIALOG_H
