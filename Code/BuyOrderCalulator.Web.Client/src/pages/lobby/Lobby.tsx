import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Row, Col, Input, Layout, Table } from 'antd';
import { Item, SaleItem } from '../../domain/domain';
import NumberFormat from 'react-number-format';
import SellModal from './SellModal'
import { DeleteOutlined } from '@ant-design/icons';
import './Lobby.less'

const { Header, Content } = Layout;

export default function Lobby(props: any) {
    const history = useHistory();
    const [allItems, setAllItems] = useState<Item[]>([]);
    const [saleItems, setSaleItems] = useState<SaleItem[]>([]);
    const [search, setSearch] = useState<string>("");

    useEffect(() => {
        fetchItems();
    }, []);

    async function fetchItems() {
        const result = await fetch("api/items");
        result.json().then(res => setAllItems(res))
            .catch(err => console.log(err));
    }

    function filteredItems(): Array<Item> {
        return allItems
            .filter(item => !saleItems.some(s => s.itemId == item.id))
            .filter(item => item.name.toUpperCase().startsWith(search.toUpperCase()))
    }

    const iskFormat = (value: number) => <NumberFormat value={value} displayType={'text'} thousandSeparator={true} prefix={'Ƶ '} />
    const numberFormat = (value: number | string) => <NumberFormat value={value} displayType={'text'} thousandSeparator={true} />

    function addSaleItem(saleItem:SaleItem) {
        var items = [...saleItems];
        items.push(saleItem);
        setSaleItems(items);
    }

    function removeSaleItem(saleItem:SaleItem) {
        var items = [...saleItems];
        setSaleItems(items.filter(x => x.itemId != saleItem.itemId));
    }

    const itemColumns = [
        { title: 'Name', dataIndex: 'name', key: 'name' },
        { title: 'Type', dataIndex: 'typeName', key: 'typeName' },
        { title: 'Unit Price', dataIndex: 'unitPrice', key: 'unitPrice', render: iskFormat },
        { title: 'Quantity', dataIndex: 'quantity', key: 'quantity', render: numberFormat },
        { title: 'Reorder Level', dataIndex: 'reorderLevel', key: 'reorderLevel', render: numberFormat },
        { title: 'Corp Credit', dataIndex: 'corpCreditMultiplier', key: 'corpCreditMultiplier', render: (value: number) => (value * 2.5) + "%" },
        { title: 'Supply Level', dataIndex: 'supplyTypeName', key: 'supplyTypeName' },
        { key: 'sell', render: (cell:null, item:Item) => <SellModal item={item} addSaleItem={addSaleItem} /> },
    ]

    function getSalePrice(saleItem: SaleItem)
    {
        let item = allItems.find(x => x.id == saleItem.itemId);
        let price = item!.unitPrice * saleItem.quantity;
        return iskFormat(price);
    }

    const saleColumns = [
        { title: 'Name', dataIndex: 'itemId', key: 'itemId', render: (cell:number) => allItems.find(x => x.id == cell)?.name },
        { title: 'Quantity', dataIndex: 'quantity', key: 'quantity' },
        { title: 'Sale Price', dataIndex: 'unitPrice', key: 'unitPrice', render: (cell:null, saleItem: SaleItem) => getSalePrice(saleItem) },
        { key: 'delete', render: (cell:null, item:SaleItem) => <DeleteOutlined onClick={() => removeSaleItem(item)} /> },
    ]

    let itemTable = <Table rowKey='id' pagination={{hideOnSinglePage: true}} className="table" rowClassName={(item) => item.supplyTypeName} columns={itemColumns} dataSource={filteredItems()} />;
    let saleTable = <Table rowKey='itemId' pagination={{hideOnSinglePage: true}} className="table" columns={saleColumns} dataSource={saleItems} locale={{emptyText: "- Empty -"}} />;

    return (
        <Layout>
            <Header className="header">
                <Row>
                    <Col flex='auto' className="title">
                        <img height='40px' src={require('../../images/nilf_logo.png')} />
                        Nilfgaard Industries
                        </Col>
                    <Col>Repp's Buy Tool</Col>
                </Row>
            </Header>
            <Content>
                <Row gutter={16}>
                    <Col flex={2}>
                        <div style={{ padding: '20px' }}>
                            <Input placeholder="Filter by name" value={search} onChange={e => setSearch(e.target.value)} />
                        </div>
                        {itemTable}
                    </Col>
                    <Col flex={3}>
                        {saleTable}
                    </Col>
                </Row>
            </Content>
        </Layout>
    )
}