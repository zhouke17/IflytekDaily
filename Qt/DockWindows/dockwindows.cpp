#include "dockwindows.h"
#include <QTextEdit>
#include <QDockWidget>

DockWindows::DockWindows(QWidget *parent)
    : QMainWindow(parent)
{
setWindowTitle(tr("DockWindows")); //设置主窗口的标题栏文字
QTextEdit *text = new QTextEdit(this);//定义一个QTextEdit 对象作为主窗口
text->setText(tr("Main Window"));
text->setAlignment (Qt::AlignCenter);
setCentralWidget (text);//将此编辑框设为主窗口的中央窗体
//停靠窗口1
QDockWidget *dock=new QDockWidget(tr("DockWindowl"),this);
//可移动
dock->setFeatures(QDockWidget::DockWidgetMovable);//设置停靠窗体的特性
dock->setAllowedAreas(Qt::LeftDockWidgetArea|Qt::RightDockWidgetArea);//只可在主窗口的左边和右边停靠
QTextEdit *te1 =new QTextEdit();
te1->setText(tr("windowl,The dock widget can be moved between docksby the user"));
dock->setWidget(te1);
addDockWidget(Qt::RightDockWidgetArea,dock);//增加停靠窗口

//停靠窗口2
dock = new QDockWidget(tr("DockWindow2"),this);
dock->setFeatures(QDockWidget::DockWidgetClosable|QDockWidget::DockWidgetFloatable);//只可在浮动和右部停靠两种状态间切换，并且不可移动
QTextEdit *te2 =new QTextEdit();
te2->setText(tr("window2,The dock widget can be detached from the mainwindow,and floated as an independent window,and can be closed"));
dock->setWidget(te2);
addDockWidget(Qt::RightDockWidgetArea,dock);

//停靠窗口3
dock=new QDockWidget(tr("DockWindow3"),this);
dock->setFeatures(QDockWidget::AllDockWidgetFeatures); //全部特性
QTextEdit *te3 = new QTextEdit();
te3->setText(tr("window3,The dock widget can be closed,moved,and floated"));
dock->setWidget(te3);
addDockWidget(Qt::RightDockWidgetArea,dock);
}

DockWindows::~DockWindows()
{

}
