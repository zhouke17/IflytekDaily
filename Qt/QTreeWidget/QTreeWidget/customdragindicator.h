#ifndef CUSTOMDRAGINDICATOR_H
#define CUSTOMDRAGINDICATOR_H

#include <QWidget>
#include <QPainter>
#include <QPen>

class CustomDragIndicator : public QWidget {
    Q_OBJECT

public:
    CustomDragIndicator(QWidget *parent = nullptr) : QWidget(parent) {
        setAttribute(Qt::WA_TransparentForMouseEvents);
    }

protected:
    void paintEvent(QPaintEvent *event) override {
        QPainter painter(this);
        painter.setPen(QPen(Qt::gray, 2, Qt::SolidLine));
        painter.drawLine(0, height() / 2, width(), height() / 2);
    }
};
#endif // CUSTOMDRAGINDICATOR_H
