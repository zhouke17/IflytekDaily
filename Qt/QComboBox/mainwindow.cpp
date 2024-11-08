#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    ui->comboBox->setEditable(true);
    ui->comboBox->addItems({"Apple", "Banana", "Cherry", "Date", "Grape", "Orange"});
    ui->comboBox->installEventFilter(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

bool MainWindow::eventFilter(QObject *wactch, QEvent *e)
{
    if(wactch == ui->comboBox)
    {
        if(e->type() == QEvent::KeyPress)
        {
            ui->comboBox->showPopup();
            ui->comboBox->setFocus();
        }
    }
    return QWidget::eventFilter(wactch, e);
}
