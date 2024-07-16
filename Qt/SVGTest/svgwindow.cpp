#include "svgwindow.h"

SvgWindow::SvgWindow(QWidget *parent): QScrollArea(parent)
{
    svgWidget = new SvgWidget;
    setWidget(svgWidget);//设置滚动区的窗体，使svgWidget成为SvgWindow的子窗口。
}

void SvgWindow::setFile(QString filename)
{
    svgWidget->load(filename);//加载新文件
    QSvgRenderer *render = svgWidget->renderer();//获取加载图片渲染后的对象
    svgWidget->resize(render->defaultSize());//使svgWidget窗体按照图片大小进行显示
}

void SvgWindow::mousePressEvent(QMouseEvent *event)
{
    mousePressPos = event->pos();
    scrollBarValuesOnMousePress.rx()=horizontalScrollBar()->value();
    scrollBarValuesOnMousePress.ry()=verticalScrollBar()->value();
}

void SvgWindow::mouseMoveEvent(QMouseEvent *event)
{
    horizontalScrollBar() -> setValue(scrollBarValuesOnMousePress.x() - event->pos().x() + mousePressPos.x());//设置滚动条x轴新位置
    verticalScrollBar() -> setValue(scrollBarValuesOnMousePress.y() - event->pos().y() + mousePressPos.y());//设置滚动条y轴新位置
    horizontalScrollBar()->update();
    verticalScrollBar()->update();
    event->accept();
}
