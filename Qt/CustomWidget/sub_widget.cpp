#include "sub_widget.h"

Sub_Widget::Sub_Widget(QWidget *parent) : QWidget(parent)
{
    pushBtn = new QPushButton(tr("开始下载"));
    pushBtn->setFixedSize(60,30);

    proBar = new QProgressBar(this);
    proBar->setRange(0,100);
    proBar->setFixedSize(180,30);

    timer = new QTimer(this);
//    connect(timer,&QTimer::timeout,this,&Sub_Widget::slt_TimeOut);
    connect(timer,SIGNAL(timeout()),this,SLOT(slt_TimeOut()));

    QHBoxLayout *layout = new QHBoxLayout(this);
//    layout ->setAlignment(Qt::AlignCenter);
    layout->addStretch();//占位（自适应）
    layout -> addWidget(pushBtn);
    layout -> setSpacing(50);//设置间距
    layout -> addWidget(proBar);
    layout->addStretch();

    connect(pushBtn,&QPushButton::pressed,[=]
            {
                timer->start(10);//启动或重新启动一个超时时间间隔为毫秒的定时器。
                proBar->reset();//重置进度条
            });
}
void Sub_Widget::slt_TimeOut()
{
    num ++;
    proBar->setValue(num);//刷新进度条
    if(num == 100)
    {
        timer->stop();
        num = 0;
    }
}
