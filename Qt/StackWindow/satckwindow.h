#ifndef SATCKWINDOW_H
#define SATCKWINDOW_H

#include <QMainWindow>
#include <QListWidget>
#include <QLabel>
#include <QStackedWidget>

class SatckWindow : public QMainWindow
{
    Q_OBJECT

public:
    SatckWindow(QWidget *parent = 0);
    ~SatckWindow();
private:
    QListWidget *list;
    QLabel *label1;
    QLabel *label2;
    QLabel *label3;
    QStackedWidget *stack;
};

#endif // SATCKWINDOW_H
