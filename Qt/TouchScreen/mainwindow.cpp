#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QEvent>
#include <QMenu>
#include <QFile>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    QFile file(":/Resource/menu.qss");
    file.open(QFile::ReadOnly);//读取qss文件，设置样式
    if(file.isOpen())
    {
        QString qss = file.readAll();
        this->setStyleSheet(qss);
    }
    file.close();

    CreateMenu();
    setAttribute(Qt::WA_AcceptTouchEvents);

}
void MainWindow::mousePressEvent(QMouseEvent *event)
{
    if(event -> button() == Qt::RightButton)
    {
        m_pMenuRD -> exec(QCursor::pos());
    }
}
void MainWindow::CreateMenu()
{
    m_pMenuRD = new QMenu(this);
    m_pActionAdd = m_pMenuRD->addAction(tr("Add"));
    m_pActionDelete = m_pMenuRD->addAction(tr("Delete"));
    m_pActionEdit = m_pMenuRD->addAction(tr("Edit"));
    connect(m_pMenuRD,SIGNAL(triggered(QAction*)),this,SLOT(SlotMenuClicked(QAction*)));
}
void MainWindow::SlotMenuClicked(QAction *action)
{
    if (action == m_pActionAdd)
    {
        ui->m_pLabel->setText(tr("Add"));
    }
    else if (action == m_pActionDelete)
    {
        ui->m_pLabel->setText(tr("Delete"));
    }
    else if (action == m_pActionEdit)
    {
        ui->m_pLabel->setText(tr("Edit"));
    }
}
bool MainWindow::event(QEvent *e)
{
    switch (e->type())
    {
    case QEvent::TouchBegin:
        timer.start();
        emit touchBegin();
        e->accept();
        return true;
    case QEvent::TouchEnd:
        if(!timer.hasExpired(3000))
            emit touchEnd();
        e->accept();
        return true;
    default:
        break;
    }
    return QWidget::event(e);
}

MainWindow::~MainWindow()
{
    delete ui;
}
