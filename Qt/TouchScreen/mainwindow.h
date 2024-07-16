#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QEvent>
#include <QElapsedTimer>
#include "qmenu.h"
#include "qevent.h"

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    virtual void mousePressEvent(QMouseEvent *event) override;
private:
    Ui::MainWindow *ui;
    QElapsedTimer timer;
    QMenu *menu;
    void CreateMenu();
private slots:
      void SlotMenuClicked(QAction*);
protected:
    bool event(QEvent *e)override;
signals:
    void touchBegin();
    void touchEnd();
private:
    //右键菜单
    QMenu* m_pMenuRD;
    QAction* m_pActionAdd;
    QAction* m_pActionDelete;
    QAction* m_pActionEdit;
};

#endif // MAINWINDOW_H
