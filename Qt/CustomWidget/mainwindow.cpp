#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QDebug>
#include <QWidget>
#include <QPushButton>
#include <QVBoxLayout>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    int ret = add->add(1,2);
    qDebug() << "a + b: " << ret << endl;


    QWidget *window = new QWidget();//窗口
    window->resize(300,400);
    window->show();
    QPushButton *pushBtn = new QPushButton(tr("按钮"),window);
    pushBtn->resize(50,60);
    pushBtn->move(50,50);
    pushBtn->show();

    QLabel *label = new QLabel(tr("占位label"));
    QVBoxLayout *vLayout = new QVBoxLayout();
    vLayout->addWidget(label);
    window->setLayout(vLayout);
}

MainWindow::~MainWindow()
{
    delete ui;
}
