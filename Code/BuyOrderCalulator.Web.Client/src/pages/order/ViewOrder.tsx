import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Row, Col, Input, Layout, Table, Button } from 'antd';
import { Order, OrderItem } from '../../domain/domain';
import NumberFormat from 'react-number-format';
import './Order.less'

const { Header, Content } = Layout;

export default function ViewOrder(props: any) {
    const history = useHistory();
    const [order, setOrder] = useState<Order>();

    useEffect(() => {
        fetchOrder(props.match.params.id);
    }, []);

    async function fetchOrder(orderGuid: string) {
        const result = await fetch(`/api/order/${orderGuid}`);
        result.json().then(res => setOrder(res))
            .catch(err => console.log(err));
    }

    function getTotalPrice(orderItem: OrderItem) {
        return orderItem.fixedUnitPrice * orderItem.quantity;
    }

    function getCorpCredit(orderItem: OrderItem) {
        return Math.ceil((orderItem.fixedCorpCreditPercent / 100) * getTotalPrice(orderItem));
    }

    const iskFormat = (value: number) => <NumberFormat value={value} displayType={'text'} thousandSeparator={true} prefix={'Ƶ '} />

    const orderItemColumns = [
        { title: 'Name', dataIndex: 'itemName', key: 'itemName' },
        { title: 'Quantity', dataIndex: 'quantity', key: 'quantity' },
        { title: 'Unit Price', dataIndex: 'fixedUnitPrice', key: 'fixedUnitPrice', render: (x: number) => iskFormat(x) },
        { title: 'Credit %', dataIndex: 'fixedCorpCreditPercent', key: 'fixedCorpCreditPercent', render: (x: number) => x + " %" },
        { title: 'Total Price', key: 'totalPrice', render: (cell: null, orderItem: OrderItem) => iskFormat(getTotalPrice(orderItem)) },
        { title: 'Total Credit', key: 'totalCredit', render: (cell: null, orderItem: OrderItem) => iskFormat(getCorpCredit(orderItem)) },
    ]

    let itemTable = order != undefined ? <Table rowKey='id' pagination={{ hideOnSinglePage: true }} className="table" columns={orderItemColumns} dataSource={order!.orderItems} /> : <></>;

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
                {itemTable}
            </Content>
        </Layout>
    )
}