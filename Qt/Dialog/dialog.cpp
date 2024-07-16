#include "dialog.h"
#include "ui_dialog.h"
#include <QBitmap>
#include <QPainter>
#include <QDesktopWidget>
#include <QMouseEvent>
#include <QPoint>

Dialog::Dialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::Dialog)
{
    ui->setupUi(this);
    setWindowFlags(Qt::FramelessWindowHint);//去除边框及标题栏
    setAttribute(Qt::WA_TranslucentBackground);//设置背景透明
}

Dialog::~Dialog()
{
    delete ui;
}
//绘制事件
void Dialog::paintEvent(QPaintEvent *e)
{
    QBitmap bmp(this->size());
    bmp.fill();
    QPainter painter(&bmp);
    painter.setRenderHint(QPainter::Antialiasing);
    painter.setBrush(Qt::red);
    painter.setPen(QPen(Qt::red, 4, Qt::SolidLine));
    painter.drawRoundedRect(bmp.rect(), 4, 4, Qt::RelativeSize);
    setMask(bmp);
}
//展示事件
void Dialog::showEvent(QShowEvent *e)
{
    // 获取屏幕几何信息并计算出窗口应该放置的位置
    QRect desktopRect = QApplication::desktop()->screenGeometry();
    if(this->parent() != nullptr){
        QWidget *pWidget = static_cast<QWidget*>(this->parent());
        if(pWidget != nullptr)
            desktopRect = pWidget->rect();
    }
    // Move the dialog box to the center of the screen
    move(desktopRect.center() - rect().center());//屏幕中央
}
//鼠标释放
void Dialog::mouseReleaseEvent(QMouseEvent *event)
{
    if (event->button() == Qt::LeftButton)
    {
        dragPosition = QPoint(-1, -1);
        event->accept();
    }
}

//鼠标按下
void Dialog::mousePressEvent(QMouseEvent *event)
{
    if (event->button() == Qt::LeftButton)
    {
        dragPosition = event->globalPos() - frameGeometry().topLeft();
        event->accept();
    }
    if (event->button() == Qt::RightButton) {
        // 设置对话框的位置为鼠标点击的位置
        QPoint clickPos = QCursor::pos();  // 获取鼠标点击的位置
        PopForm m_popForm;
        m_popForm.move(clickPos);
        if(m_popForm.exec() == QDialog::Accepted)
        {

        }
    }
}
//鼠标拖动
void Dialog::mouseMoveEvent(QMouseEvent *event)
{
    if(this->windowState() == Qt::WindowNoState)
    {
        if (event->buttons() &Qt::LeftButton)
        {
            if (dragPosition != QPoint(-1, -1))
            {
                QPoint point = event->globalPos() - dragPosition;
                move(point);
            }
            event->accept();
        }
    }
}
