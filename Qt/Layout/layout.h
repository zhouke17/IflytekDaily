#ifndef LAYOUT_H
#define LAYOUT_H

#include <QDialog>
#include <QLabel>
#include <QLineEdit>
#include <QComboBox>
#include <QTextEdit>
#include <QGridLayout>
#include <QPushButton>
#include <QPixmap>

class Layout : public QDialog
{
    Q_OBJECT

public:
    Layout(QWidget *parent = 0);
    ~Layout();
private:
    //左侧
    QLabel *UserNameLabel;
    QLabel *Namelabel;
    QLabel *SexLabel;
    QLabel *DepartmentLabel;
    QLabel *AgeLabel;
    QLabel *OtherLabel;
    QLineEdit *UserNameLineEdit;
    QLineEdit *NameLineEdit;
    QComboBox *SexComboBox;
    QTextEdit *DepartmentTextEdit;
    QLineEdit *AgeLineEdit;
    QGridLayout *LeftLayout;
    //右侧
    QLabel *HeadLabel;
    QLabel *HeadIconLabel;
    QPushButton *UpdateHeadBtn;
    QHBoxLayout *TopRightLayout;
    QLabel *IntroductionLabel;
    QTextEdit *IntroductionTextEdit;
    QVBoxLayout *RightLayout;
    //底部
    QPushButton *OkBtn;
    QPushButton *CancelBtn;
    QHBoxLayout *ButtomLayout;
};

#endif // LAYOUT_H
