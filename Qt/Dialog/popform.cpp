#include "popform.h"
#include "ui_popform.h"

PopForm::PopForm(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::PopForm)
{
    ui->setupUi(this);
    this->setWindowFlags(Qt::Popup);
}

PopForm::~PopForm()
{
    delete ui;
}

void PopForm::on_pushButton_clicked()
{
    this->accept();
}
