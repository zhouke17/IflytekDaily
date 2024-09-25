#ifndef TREEITEMMIMEDATA_H
#define TREEITEMMIMEDATA_H

#include <QMimeData>

class QTreeWidgetItem;
class TreeItemMimeData:QMimeData
{
    Q_OBJECT
public:
    TreeItemMimeData():QMimeData(){}
    QStringList formats() const {return m_format ;}
    const QTreeWidgetItem * DragItemData() const { return  m_pDragItem;}
    void SetDragData(QString mimeType,QTreeWidgetItem * pItem);
private:
    QStringList m_format;
    const QTreeWidgetItem * m_pDragItem;
protected:
    QVariant retrieveData(const QString &mimetype, QVariant::Type preferredType) const ;
};

#endif // TREEITEMMIMEDATA_H
