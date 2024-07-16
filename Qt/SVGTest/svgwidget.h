#ifndef SVGWIDGET_H
#define SVGWIDGET_H
#include <QtSvg>
#include <QSvgWidget>
#include <QSvgRenderer>

//显示SVG图片
class SvgWidget : public QSvgWidget
{
    Q_OBJECT
public:
    SvgWidget(QWidget *parent = 0);
    void wheelEvent(QWheelEvent *);//响应鼠标滚轮事件

private:
    QSvgRenderer *render;//用于图片显示尺寸
};

#endif // SVGWIDGET_H
