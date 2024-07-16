#ifndef DIALOG_H
#define DIALOG_H

#include <QDialog>
#include "popform.h"

namespace Ui {
class Dialog;
}

class Dialog : public QDialog
{
    Q_OBJECT

public:
    explicit Dialog(QWidget *parent = nullptr);
    ~Dialog();

private:
    Ui::Dialog *ui;
    QPoint dragPosition;
protected:
    void mouseMoveEvent(QMouseEvent*event) override;
    void mousePressEvent(QMouseEvent*event) override;
    void mouseReleaseEvent(QMouseEvent*event) override;
    void paintEvent(QPaintEvent* e) override;
    void showEvent(QShowEvent *e) override;

private:
    PopForm *m_popForm;
};

#endif // DIALOG_H
