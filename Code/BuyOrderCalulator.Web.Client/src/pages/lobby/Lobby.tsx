import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Row, Col, Input, Layout, Table } from 'antd';
import { Item } from '../../domain/domain';
import NumberFormat from 'react-number-format';
import './Lobby.less'

const { Header, Footer, Sider, Content } = Layout;

export default function Lobby(props: any) {
    const history = useHistory();
    const [allItems, setAllItems] = useState<Item[]>([]);
    const [items, setItems] = useState<Item[]>([]);
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
        return allItems.filter(x => x.name.toUpperCase().startsWith(search.toUpperCase()))
    }


    const iskFormat = (value: number,) => <NumberFormat value={value} displayType={'text'} thousandSeparator={true} prefix={'Ƶ '} />
    const numberFormat = (value: number,) => <NumberFormat value={value} displayType={'text'} thousandSeparator={true} />

    const columns = [
        { title: 'Name', dataIndex: 'name', key: 'name' },
        { title: 'Type', dataIndex: 'typeName', key: 'typeName' },
        { title: 'Unit Price', dataIndex: 'unitPrice', key: 'unitPrice', render: iskFormat },
        { title: 'Quantity', dataIndex: 'quantity', key: 'quantity', render: numberFormat },
        { title: 'Reorder Level', dataIndex: 'reorderLevel', key: 'reorderLevel', render: numberFormat },
        { title: 'Reorder Credit Value', dataIndex: 'reorderCreditValue', key: 'reorderCreditValue', render: numberFormat },
        { title: 'Supply Level', dataIndex: 'supplyTypeName', key: 'supplyTypeName' },
    ]

    let table = <Table className="table" rowClassName={ (item) => item.supplyTypeName } columns={columns} dataSource={filteredItems()} />;

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
                    <Col flex='auto'>
                        <div style={{ padding: '20px' }}>
                            <Input placeholder="Filter by name" value={search} onChange={e => setSearch(e.target.value)} />
                        </div>
                        {table}
                    </Col>
                    <Col flex={'300px'}>Basket?!</Col>
                </Row>

            </Content>
        </Layout>
    )
}