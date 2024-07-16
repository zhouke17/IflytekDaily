#ifndef POPFORM_H
#define POPFORM_H

#include <QDialog>

namespace Ui {
class PopForm;
}

class PopForm : public QDialog
{
    Q_OBJECT

public:
    explicit PopForm(QWidget *parent = nullptr);
    ~PopForm();

private slots:
    void on_pushButton_clicked();

private:
    Ui::PopForm *ui;
};

#endif // POPFORM_H
