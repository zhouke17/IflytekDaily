#ifndef UPANDDOWNTREEWIDGET_H
#define UPANDDOWNTREEWIDGET_H

#include <QWidget>
#include <QApplication>
#include <QTreeWidget>
#include <QTreeWidgetItem>
#include <QPushButton>
#include <QVBoxLayout>

class UpandDownTreeWidget : public QWidget
{
    Q_OBJECT
public:
    explicit UpandDownTreeWidget(QWidget *parent = nullptr) {
        QVBoxLayout *layout = new QVBoxLayout(this);

        treeWidget = new QTreeWidget(this);
        treeWidget->setColumnCount(1);
        QStringList headers;
        headers << "Items";
        treeWidget->setHeaderLabels(headers);

        // 添加示例项
//        for (int i = 0; i < 5; ++i) {
//            QTreeWidgetItem *item = new QTreeWidgetItem(treeWidget);
//            item->setText(0, QString("Item %1").arg(i));
//        }

        // 添加顶级项目
        QTreeWidgetItem *topLevelItem1 = new QTreeWidgetItem(treeWidget, {"Item 1"});
        QTreeWidgetItem *topLevelItem2 = new QTreeWidgetItem(treeWidget, {"Item 2"});

        // 添加非顶级项目
        QTreeWidgetItem *childItem1 = new QTreeWidgetItem(topLevelItem1, {"Child 1"});
        QTreeWidgetItem *childItem2 = new QTreeWidgetItem(topLevelItem1, {"Child 2"});
        QTreeWidgetItem *childItem3 = new QTreeWidgetItem(topLevelItem2, {"Child 3"});

        QPushButton *moveUpButton = new QPushButton("Move Up", this);
        QPushButton *moveDownButton = new QPushButton("Move Down", this);

        layout->addWidget(treeWidget);
        layout->addWidget(moveUpButton);
        layout->addWidget(moveDownButton);

        connect(moveUpButton, &QPushButton::clicked, this, &UpandDownTreeWidget::moveUp);
        connect(moveDownButton, &QPushButton::clicked, this, &UpandDownTreeWidget::moveDown);
    }

private slots:
    void moveUp() {
//        QList<QTreeWidgetItem *> selectedItems = treeWidget->selectedItems();
//        if (selectedItems.isEmpty()) return;

//        foreach (QTreeWidgetItem *item, selectedItems) {
//            QTreeWidgetItem *prev = treeWidget->itemAbove(item);
//            if (prev) {
//                int index = item->parent() ? item->parent()->indexOfChild(item) : treeWidget->indexOfTopLevelItem(item);
//                if (item->parent()) {
//                    item->parent()->removeChild(item);
//                    item->parent()->insertChild(index - 1, item);
//                } else {
//                    treeWidget->takeTopLevelItem(index);
//                    treeWidget->insertTopLevelItem(index - 1, item);
//                }
//            }
//        }
moveItem(-1);
    }

    void moveDown() {
//        QList<QTreeWidgetItem *> selectedItems = treeWidget->selectedItems();
//        if (selectedItems.isEmpty()) return;

//        foreach (QTreeWidgetItem *item, selectedItems) {
//            QTreeWidgetItem *next = treeWidget->itemBelow(item);
//            if (next) {
//                int index = item->parent() ? item->parent()->indexOfChild(item) : treeWidget->indexOfTopLevelItem(item);
//                if (item->parent()) {
//                    item->parent()->removeChild(item);
//                    item->parent()->insertChild(index + 1, item);
//                } else {
//                    treeWidget->takeTopLevelItem(index);
//                    treeWidget->insertTopLevelItem(index + 1, item);
//                }
//            }
//        }
moveItem(1);
    }

    void moveItem(int direction) {
        QList<QTreeWidgetItem*> selectedItems = treeWidget->selectedItems();
        if (selectedItems.isEmpty()) return;

        QTreeWidgetItem *item = selectedItems.first();
        QTreeWidgetItem *parent = item->parent();
        if (!parent) return; // Only works for non-top-level items

        int index = parent->indexOfChild(item);
        int newIndex = index + direction;

        if (newIndex < 0 || newIndex >= parent->childCount()) return;

        parent->takeChild(index);
        parent->insertChild(newIndex, item);
        treeWidget->setCurrentItem(item);
    }

private:
    QTreeWidget *treeWidget;
};


#endif // UPANDWODNTREEWIDGET_H
