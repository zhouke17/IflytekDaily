#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QDateTime>
#include <QLabel>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    connect(ui->RadioBlack,SIGNAL(clicked()),this,SLOT(setTextFontColor()));
    connect(ui->RadioRed,SIGNAL(clicked()),this,SLOT(setTextFontColor()));
    connect(ui->RadioBlue,SIGNAL(clicked()),this,SLOT(setTextFontColor()));

//    connect(ui->quitBtn,"clicked()",this,"quite()");//与下方等同
    connect(ui->quitBtn, &QPushButton::clicked, this, [=](){
        this->close();
    });

    connect(this,&MainWindow::mySignal,this,&MainWindow::mySlot);
    emit mySignal("触发mySignal信号！");

    connect(ui->myhorizontalSlider,&QSlider::valueChanged,this,&MainWindow::mySlot2);


    QLabel *label = new QLabel();
    QDateTime *time = new QDateTime(QDateTime::currentDateTime());
    label->setText(time->date().toString());
    label->show();
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::mySlot(const QString &value)
{
    ui->mytextEdit->setText(value);
}
void MainWindow::mySlot2(int value)
{
    ui->mytextEdit->setText(QString::number(value));
}
void MainWindow::setTextFontColor()
{
    QPalette plet = ui->plainTextEdit->palette();
    if(ui->RadioBlack->isChecked())
        plet.setColor(QPalette::Text,Qt::black);
    else if(ui->RadioRed->isChecked())
        plet.setColor(QPalette::Text,Qt::red);
    else  if(ui->RadioBlue->isChecked())
        plet.setColor(QPalette::Text,Qt::blue);
    else
        plet.setColor(QPalette::Text,Qt::black);
    ui->plainTextEdit->setPalette(plet);
}

void MainWindow::quite()
{
    this->close();
}

void MainWindow::on_CheckUnderLine_clicked(bool checked)
{
  QFont font = ui->plainTextEdit->font();
  font.setUnderline(checked);
  ui->plainTextEdit->setFont(font);
}

void MainWindow::on_CheckItalic_clicked(bool checked)
{
    QFont font = ui->plainTextEdit->font();
    font.setItalic(checked);
    ui->plainTextEdit->setFont(font);
}

void MainWindow::on_CheckBold_clicked(bool checked)
{
    QFont font = ui->plainTextEdit->font();
    font.setBold(checked);
    ui->plainTextEdit->setFont(font);
}

