#ifndef SUB_WIDGET_H
#define SUB_WIDGET_H

#include <QWidget>
#include <QHBoxLayout>
#include <QPushButton>
#include <QProgressBar>
#include <QTimer>


class Sub_Widget : public QWidget
{
    Q_OBJECT
public:
    explicit Sub_Widget(QWidget *parent = nullptr);
private:
    QPushButton *pushBtn;
    QProgressBar *proBar;
    QTimer *timer;
    int num = 0;
signals:

public slots:
        void slt_TimeOut();
};

#endif // SUB_WIDGET_H
