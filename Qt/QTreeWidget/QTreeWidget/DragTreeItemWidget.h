#ifndef DRAGTREEITEMWIDGET_H
#define DRAGTREEITEMWIDGET_H

#include <QWidget>
#include <QTreeWidget>
#include <QTreeWidgetItem>
#include <QDrag>
#include <QMimeData>
#include <QDropEvent>
#include <QMimeData>
#include <QDragEnterEvent>
#include <QDragMoveEvent>
#include <QDropEvent>
#include "customdragindicator.h"
#include "qdebug.h"

/**
 * @brief The DragTreeItemWidget class  自定义拖拽绘制样式
 */
class DragTreeItemWidget : public QTreeWidget
{
    Q_OBJECT
public:
    explicit DragTreeItemWidget(QWidget *parent = nullptr){
           CustomDragIndicator *indicator = new CustomDragIndicator(this);
           dragIndicator = indicator;
           dragIndicator->hide();
    }
    enum DropIndicatorPosition { OnItem, AboveItem, BelowItem, OnViewport };

private:
    CustomDragIndicator *dragIndicator;
    QRect m_dropRect;// 用于存储矩形框的位置
    DropIndicatorPosition m_position;
    int m_dragIndex;
signals:

public slots:

protected:
    void startDrag(Qt::DropActions supportedActions)  override{
        item = currentItem();
        if (!item) return;
        m_dragIndex = currentIndex().row();
        QDrag *drag = new QDrag(this);
        QMimeData *mimeData = new QMimeData;

        // 将拖拽的项目索引信息存储在 MIME 数据中
        mimeData->setText(item->text(0));
        drag->setMimeData(mimeData);

        drag->exec(Qt::MoveAction);
    }
    void paintEvent(QPaintEvent *event) override {
        QTreeWidget::paintEvent(event); // 调用基类的绘制方法

        if (!m_dropRect.isNull()) {
            QPainter painter(viewport());
            painter.setPen(QPen(Qt::blue, 2, Qt::DashLine));
            painter.drawRect(m_dropRect); // 绘制矩形框
            qDebug() << "paintEvent normal";
        }
//        else
//        {
//            QPainter painter(viewport());
//            painter.setPen(QPen(Qt::red, 2, Qt::DashLine));
//            painter.drawRect(m_dropRect); // 绘制矩形框
//            qDebug() << "paintEvent unnormal";
//        }
    }
    void dragEnterEvent(QDragEnterEvent *event) override {
        if (event->mimeData()->hasText()) {
            event->acceptProposedAction();
//            QTreeWidget::dragEnterEvent(event);//拖拽被禁用
        } else {
            event->ignore();
            qDebug() << "dragEnterEvent ignore";
        }
    }
    void dragMoveEvent(QDragMoveEvent *event) override {
        if (event->mimeData()->hasText()) {

            event->acceptProposedAction();

            QModelIndex model = indexAt(event->pos());
            QString item = model.data(0).toString();

            QRect rect = visualRect(model);

            qDebug() << "current rect:" << "x:" << rect.x() << "y:" << rect.y() << "width:" << rect.width() << "height:" << rect.height();

            if(rect.top() == event->pos().y())
            {
                m_position = DropIndicatorPosition::AboveItem;
            }
            else if(rect.bottom() == event->pos().y())
            {
                m_position = DropIndicatorPosition::BelowItem;
            }
            else
            {
                m_position = DropIndicatorPosition::OnItem;
            }
//            dragIndicator->setGeometry(QRect(0, event->pos().y() + 20, 300, 1));
            switch (m_position) {
            case OnItem:
                qDebug() << "position : onItem" << item;
//                dragIndicator->show();

                m_dropRect = rect;
                break;
            case AboveItem:
                qDebug() << "position : AboveItem" << item;
//                dragIndicator->show();

                m_dropRect = QRect(rect.left(),rect.top(),rect.width(),0);
                break;
            case BelowItem:
                qDebug() << "position : BelowItem" << item;
//                dragIndicator->show();

                m_dropRect = QRect(rect.left(),rect.bottom(),rect.width(),0);
                break;
            default:
                qDebug() << "position : other" << item;
//                dragIndicator->show();

                m_dropRect = QRect();
                break;
            }
//            QTreeWidget::dragMoveEvent(event);//影响了dropEvent事件的接收，导致其无法被调用，并且导致绘制以及拖拽失败
            viewport()->update();//重绘
        } else {
            event->ignore();
            qDebug() << "dragMoveEvent ignore";
        }
    }

    void dropEvent(QDropEvent *event) override {

        QTreeWidgetItem *targetItem = itemAt(event->pos());

        QModelIndex model = indexAt(event->pos());

        bool isForward = m_dragIndex > model.row();

        if (model.isValid()) {
            qDebug() << "targetItem item:" << model.data(0).toString();

            QTreeWidgetItem *draggedItem = item;
            // 移动项目到目标项下
            QModelIndex parentModel = model.parent();
            QTreeWidgetItem *parent = targetItem->parent();
            if (parentModel.isValid() && parent) {
                int targetIndex = 0;
                if(m_position == DropIndicatorPosition::OnItem)
                {
                    targetIndex = model.row() + 1;//默认放到当前项下方
                }
                else if(m_position == DropIndicatorPosition::AboveItem)
                {
                    if(isForward)
                        targetIndex = model.row() - 1 ? -1 : 0;
                    else {
                        targetIndex = model.row() - 1;
                    }
                }
                else {
                    if(isForward)
                        targetIndex = model.row() + 1;
                    else {
                        targetIndex = model.row();
                    }
                }

                parent->removeChild(draggedItem);
                parent->insertChild(targetIndex,draggedItem);
            }
            setCurrentItem(draggedItem);
            // dragIndicator->hide();
        }
        m_dropRect = QRect(); // 清空矩形框
        viewport()->update();//重绘
        event->acceptProposedAction();
//        QTreeWidget::dropEvent(event);
    }

private:
    QTreeWidgetItem *item;
};

#endif // DRAGTREEITEMWIDGET_H
