#ifndef ADD_H
#define ADD_H

#include <QWidget>


class Add : public QWidget
{
    Q_OBJECT
public:
    explicit Add(QWidget *parent = nullptr);

    int add(int a,int b);
signals:

public slots:
};

#endif // ADD_H
