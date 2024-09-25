#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "UpandDownTreeWidget.h"
#include "DragTreeWidget.h"
#include "DragEventFilter.h"
#include "DragTreeItemWidget.h"

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_Drag_pushButton_clicked()
{
    DragTreeWidget *widget = new DragTreeWidget(this);
    widget->setFixedSize(400,300);
    widget->show();
}

void MainWindow::on_UpDwon_pushButton_clicked()
{
    UpandDownTreeWidget *widget = new UpandDownTreeWidget(this);
    widget->show();
}

//事件监控拖拽，不重写控件
void MainWindow::on_DrapEventFilter_pushButton_clicked()
{
    QTreeWidget *treeWidget = new QTreeWidget(this);
    treeWidget->setColumnCount(1);
    treeWidget->setHeaderLabels(QStringList() << "Column 1");
    treeWidget->setDragEnabled(true);
    treeWidget->setDragDropMode(QAbstractItemView::InternalMove);
    treeWidget->setDefaultDropAction(Qt::MoveAction);
    treeWidget->setDropIndicatorShown(true);

    // 添加顶级项
    QTreeWidgetItem *item1 = new QTreeWidgetItem(treeWidget, QStringList() << "Item 1");
    QTreeWidgetItem *item2 = new QTreeWidgetItem(treeWidget, QStringList() << "Item 2");
    QTreeWidgetItem *item3 = new QTreeWidgetItem(treeWidget, QStringList() << "Item 3");

    //添加子级项
    QTreeWidgetItem *subItem1 = new QTreeWidgetItem(item1, {"SubItem 1"});
    QTreeWidgetItem *subItem2 = new QTreeWidgetItem(item1, {"SubItem 2"});
    QTreeWidgetItem *subItem3 = new QTreeWidgetItem(item1, {"SubItem 3"});
    QTreeWidgetItem *subItem4 = new QTreeWidgetItem(item1, {"SubItem 4"});
    QTreeWidgetItem *subItem5 = new QTreeWidgetItem(item1, {"SubItem 5"});

    CustomDragIndicator *indicator = new CustomDragIndicator(this);

    // 创建并安装事件过滤器
    DragEventFilter *filter = new DragEventFilter(treeWidget,this,indicator);
    treeWidget->viewport()->installEventFilter(filter);

    treeWidget->setFixedSize(400,300);
    // 显示树状控件
    treeWidget->show();
}

//重写QTreeWidget
void MainWindow::on_OverrideTreeWidget_pushButton_clicked()
{
    DragTreeItemWidget *widget = new DragTreeItemWidget(this);
    widget->setColumnCount(1);
    widget->setHeaderLabels(QStringList() << "Column 1");
    widget->setDragEnabled(true);
    widget->setDragDropMode(QAbstractItemView::InternalMove);
    widget->setDefaultDropAction(Qt::MoveAction);

    // 添加顶级项
    QTreeWidgetItem *item1 = new QTreeWidgetItem(widget, QStringList() << "Item 1");
    QTreeWidgetItem *item2 = new QTreeWidgetItem(widget, QStringList() << "Item 2");
    QTreeWidgetItem *item3 = new QTreeWidgetItem(widget, QStringList() << "Item 3");

    //添加子级项
    QTreeWidgetItem *subItem1 = new QTreeWidgetItem(item1, {"SubItem 1"});
    QTreeWidgetItem *subItem2 = new QTreeWidgetItem(item1, {"SubItem 2"});
    QTreeWidgetItem *subItem3 = new QTreeWidgetItem(item1, {"SubItem 3"});
    QTreeWidgetItem *subItem4 = new QTreeWidgetItem(item1, {"SubItem 4"});
    QTreeWidgetItem *subItem5 = new QTreeWidgetItem(item1, {"SubItem 5"});

    widget->setFixedSize(400,300);
    widget->show();
}

//原始拖拽
void MainWindow::on_Origin_pushButton_clicked()
{
    QTreeWidget *tree = new QTreeWidget(this);
    tree->setHeaderLabels(QStringList() << "parent");

    tree->setDragEnabled(true);
    tree->setDragDropMode(QAbstractItemView::InternalMove);
    tree->setDefaultDropAction(Qt::MoveAction);

    QTreeWidgetItem *parent1 = new QTreeWidgetItem(tree,QStringList() << "parent1");
    QTreeWidgetItem *parent2 = new QTreeWidgetItem(tree,QStringList() << "parent2");
    QTreeWidgetItem *parent3 = new QTreeWidgetItem(tree,QStringList() << "parent3");


    QTreeWidgetItem *child1 = new QTreeWidgetItem(parent1,QStringList() << "child1");
    QTreeWidgetItem *child2 = new QTreeWidgetItem(parent1,QStringList() << "child2");
    QTreeWidgetItem *child3 = new QTreeWidgetItem(parent1,QStringList() << "child3");
    QTreeWidgetItem *child4 = new QTreeWidgetItem(parent1,QStringList() << "child4");
    QTreeWidgetItem *child5 = new QTreeWidgetItem(parent1,QStringList() << "child5");
    QTreeWidgetItem *child6 = new QTreeWidgetItem(parent1,QStringList() << "child6");


    tree->setFixedSize(400,300);
    tree->show();

}
