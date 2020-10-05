import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Row, Col, Input, Layout, Table, Button } from 'antd';
import { CommonData, Item, SaleItem } from '../../domain/domain';
import NumberFormat from 'react-number-format';
import AdminEditItem from './AdminEditItem'
import { DeleteOutlined } from '@ant-design/icons';
import { CheckSquareOutlined, CloseSquareOutlined } from '@ant-design/icons';
import './Order.less'

const { Header, Content } = Layout;

export default function Admin(props: any) {
    const history = useHistory();
    const [allItems, setAllItems] = useState<Item[]>([]);
    const [saleItems, setSaleItems] = useState<SaleItem[]>([]);
    const [search, setSearch] = useState<string>("");
    const [commonData, setCommonData] = useState<CommonData>();

    useEffect(() => {
        fetchItems();
        fetchCommon();
    }, []);

    async function fetchCommon() {
        const result = await fetch("/api/admin/commonData");
        result.json().then(res => setCommonData(res))
            .catch(err => console.log(err));
    }

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

    let activeToggle = (item: Item) => <Button type={item.isActive ? "primary" : "default"} onClick={() => toggleIsActive(item)} shape="circle" icon={item.isActive ? <CheckSquareOutlined /> : <CloseSquareOutlined />} />

    async function toggleIsActive(item: Item) {
        await fetch("api/admin/toggleActive", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ ItemId: item.id })
        });

        let items = [...allItems];
        let itemToUpdate = items.find(x => x.id == item.id);
        itemToUpdate!.isActive = !item.isActive;
        setAllItems(items);
    }

    const itemColumns = [
        { title: 'Enabled', dataIndex: 'isActive', key: 'isActive', render: (cell: null, item: Item) => activeToggle(item) },
        { title: 'Name', dataIndex: 'name', key: 'name' },
        { title: 'Type', dataIndex: 'typeName', key: 'typeName' },
        { title: 'Market Price', dataIndex: 'marketPrice', key: 'marketPrice', render: iskFormat },
        // { title: 'Quantity', dataIndex: 'quantity', key: 'quantity', render: numberFormat },
        // { title: 'Reorder Level', dataIndex: 'reorderLevel', key: 'reorderLevel', render: numberFormat },
        { title: 'Corp Credit', dataIndex: 'corpCreditPercent', key: 'corpCreditPercent', render: (value: number) => value + "%" },
        { title: 'Supply Level', dataIndex: 'supplyTypeName', key: 'supplyTypeName' },
        { key: 'edit', render: (cell: null, item: Item) => <AdminEditItem item={item} commonData={commonData} fetchItems={fetchItems}  /> },
    ]

    function getSalePrice(saleItem: SaleItem) {
        let item = allItems.find(x => x.id == saleItem.itemId);
        return item!.unitPrice * saleItem.quantity;
    }

    let itemTable = <Table rowKey='id' pagination={{ hideOnSinglePage: true }} className="table" rowClassName={(item) => item.supplyTypeName} columns={itemColumns} dataSource={filteredItems()} />;

    return (
        <Layout>
            <Header className="header">
                <Row>
                    <Col flex='auto' className="title">
                        <img src={require('../../images/nilf_banner.png')} />
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
                        <div className="cart">
                            <div className="user">
                                <span className="name">{props.user?.username}<span className="discrim">#{props.user?.discriminator}</span></span>
                                <img src={props.user?.avatarLink} />
                            </div>
                        </div>
                    </Col>
                </Row>
            </Content>
        </Layout>
    )
}