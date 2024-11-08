#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QPixmap>
#include <QBitmap>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    QPixmap pix;
    pix.load(":/Images/owl.png",0,Qt::AvoidDither|Qt::ThresholdDither | Qt::ThresholdAlphaDither);
    resize(pix.size());
    setMask(QPixmap(pix.mask()));


}

MainWindow::~MainWindow()
{
    delete ui;
}
