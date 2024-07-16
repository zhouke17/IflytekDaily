#include "add.h"
#include <QDebug>

Add::Add(QWidget *parent) : QWidget(parent)
{

}
int Add::add(int a, int b)
{
    qDebug() << "a: " << a << endl;
    qDebug() << "b: " << b << endl;
    return a + b;
}
