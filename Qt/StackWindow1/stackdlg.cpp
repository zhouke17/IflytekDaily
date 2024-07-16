#include "stackdlg.h"
#include <QHBoxLayout>

StackDlg::StackDlg(QWidget *parent)
    : QDialog(parent)
{
    setWindowTitle(tr("StackedWidget"));

    //新建qlistwidget控件，并创建三个条目作为选项。
    list = new QListWidget(this);
    list -> insertItem(0,tr("window1"));
    list -> insertItem(1,tr("window2"));
    list -> insertItem(2,tr("window3"));

    //创建三个qlabel控件，作为堆栈窗口的标记
    label1 = new QLabel(tr("windowTest1"));
    label2 = new QLabel(tr("windowTest2"));
    label3 = new QLabel(tr("windowTest3"));

    //创建qstackedwidget堆栈窗体对象，并将label依次放入其中
    stack = new QStackedWidget(this);
    stack -> addWidget(label1);
    stack -> addWidget(label2);
    stack -> addWidget(label3);

    //进行布局
    QHBoxLayout *mainLayout = new QHBoxLayout(this);
    mainLayout -> setMargin(5);//设置边距为5
    mainLayout -> setSpacing(5);//设置控件间距为5
    mainLayout -> addWidget(list);
    mainLayout -> addWidget(stack,0,Qt::AlignCenter);
    mainLayout -> setStretchFactor(list,1);//
    mainLayout -> setStretchFactor(stack,3);//设置可伸缩
    connect(list,SIGNAL(currentRowChanged(int)),stack,SLOT(setCurrentIndex(int)));//QListWidget的currentRowChanged（）信号与堆栈窗体的setCurrentIndex（）槽函数连接

}

StackDlg::~StackDlg()
{

}
