import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Row, Col, Input, Layout, Table, Button } from 'antd';
import { Item, SaleItem } from '../../domain/domain';
import NumberFormat from 'react-number-format';
import SellModal from './SellModal'
import { DeleteOutlined } from '@ant-design/icons';
import './Order.less'

const { Header, Content } = Layout;

export default function Order(props: any) {
    const history = useHistory();
    const [allItems, setAllItems] = useState<Item[]>([]);
    const [saleItems, setSaleItems] = useState<SaleItem[]>([]);
    const [search, setSearch] = useState<string>("");

    useEffect(() => {
        fetchItems();
    }, []);

    async function fetchItems() {
        const result = await fetch("/api/items");
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

    function addSaleItem(saleItem: SaleItem) {
        var items = [...saleItems];
        items.push(saleItem);
        setSaleItems(items);
    }

    function removeSaleItem(saleItem: SaleItem) {
        var items = [...saleItems];
        setSaleItems(items.filter(x => x.itemId != saleItem.itemId));
    }

    const itemColumns = [
        { title: 'Name', dataIndex: 'name', key: 'name' },
        { title: 'Type', dataIndex: 'typeName', key: 'typeName' },
        { title: 'Unit Price', dataIndex: 'unitPrice', key: 'unitPrice', render: iskFormat },
        { title: 'Quantity', dataIndex: 'quantity', key: 'quantity', render: numberFormat },
        { title: 'Reorder Level', dataIndex: 'reorderLevel', key: 'reorderLevel', render: numberFormat },
        { title: 'Corp Credit', dataIndex: 'corpCreditPercent', key: 'corpCreditPercent', render: (value: number) => value + "%" },
        { title: 'Supply Level', dataIndex: 'supplyTypeName', key: 'supplyTypeName' },
        { key: 'sell', render: (cell: null, item: Item) => <SellModal item={item} addSaleItem={addSaleItem} /> },
    ]

    function getSalePrice(saleItem: SaleItem) {
        let item = allItems.find(x => x.id == saleItem.itemId);
        return item!.unitPrice * saleItem.quantity;
    }

    function getCorpCredit(saleItem: SaleItem) {
        let item = allItems.find(x => x.id == saleItem.itemId);
        return Math.ceil((item!.corpCreditPercent / 100) * getSalePrice(saleItem));
    }

    async function submitOrder() {
        const result = await fetch("api/order", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(saleItems)
        });
        result.text().then(res => alert(res))
            .catch(err => console.log(err));
    }

    const saleColumns = [
        { title: 'Name', dataIndex: 'itemId', key: 'itemId', render: (cell: number) => allItems.find(x => x.id == cell)?.name },
        { title: 'Quantity', dataIndex: 'quantity', key: 'quantity' },
        { title: 'Sale Price', key: 'salePrice', render: (cell: null, saleItem: SaleItem) => iskFormat(getSalePrice(saleItem)) },
        { key: 'delete', render: (cell: null, item: SaleItem) => <DeleteOutlined onClick={() => removeSaleItem(item)} /> },
    ]

    let itemTable = <Table rowKey='id' pagination={{ hideOnSinglePage: true }} className="table" rowClassName={(item) => item.supplyTypeName} columns={itemColumns} dataSource={filteredItems()} />;
    let saleTable = <Table rowKey='itemId' pagination={{ hideOnSinglePage: true }} className="table" columns={saleColumns} dataSource={saleItems} locale={{ emptyText: "- Empty -" }} />;
    let totalSale = saleItems.length ? saleItems.map(x => getSalePrice(x)).reduce((total, item) => total + item) : 0;
    let totalCredit = saleItems.length ? saleItems.map(x => getCorpCredit(x)).reduce((total, item) => total + item) : 0;
    let summary = saleItems.length ? <div className="summary">
        <div className="total">Total sale: {iskFormat(totalSale)}</div>
        <div className="total">Corp credit: {iskFormat(totalCredit)}</div>
        <div className="note">note: prices may have changed since starting this form</div>
        <Button block type="primary" onClick={submitOrder}>Submit Order</Button>
    </div> : <></>;


    return (
        <Layout>
            <Header className="header">
                <Row>
                    <Col flex='auto' className="title">
                        <img src={require('../../images/nilf_banner.png')} />
                    </Col>
                    <Col>Repp's Buy Tool {props.user?.name}</Col>
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
                        <div className="cart">
                            {saleTable}
                            {summary}
                        </div>
                    </Col>
                </Row>
            </Content>
        </Layout>
    )
}