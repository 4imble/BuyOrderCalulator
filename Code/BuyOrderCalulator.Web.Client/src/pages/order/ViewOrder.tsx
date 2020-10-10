import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Row, Col, Tag, Layout, Table, Button } from 'antd';
import { Order, OrderItem, OrderStatus } from '../../domain/domain';
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

    async function submitOrder(state: OrderStatus) {
        await fetch("/api/order/processOrder", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ OrderGuid: order!.guid, State: state, DiscordId: props.user.discordId, AccessToken: props.user.accessToken })
        });
       
        let orderClone: Order = {... order} as Order;
        orderClone.state = state;
        setOrder(orderClone);
    }

    function getStatusColor() {
        let orderState = order ? order.state : 2;
        switch (orderState) {
            case 0: return "#e19f3c";
            case 1: return "#87d068";
            case 2: return "#fe3939";
        }
    }

    const buttonsToShow = () => {
        if(!props.user?.isAdmin)
            return <></>;

        if (order?.state == OrderStatus.Open)
            return <><Button block type="primary" onClick={() => submitOrder(OrderStatus.Complete)}>Complete Order</Button>
            <Button block type="default" onClick={() => submitOrder(OrderStatus.Cancelled)}>Cancel Order</Button></>
        if (order?.state == OrderStatus.Complete)
            return<><Button block type="default" onClick={() => submitOrder(OrderStatus.Open)}>Open Order</Button>
            <Button block type="default" onClick={() => submitOrder(OrderStatus.Cancelled)}>Cancel Order</Button></>
        if (order?.state == OrderStatus.Cancelled)
            return <><Button block type="default" onClick={() => submitOrder(OrderStatus.Complete)}>Complete Order</Button>
            <Button block type="default" onClick={() => submitOrder(OrderStatus.Open)}>Open Order</Button></>
        return <></>
    }

    let totalSale = order ? order!.orderItems.map(x => getTotalPrice(x)).reduce((total, item) => total + item) : 0;
    let totalCredit = order ? order!.orderItems.map(x => getCorpCredit(x)).reduce((total, item) => total + item) : 0;
    let summary = <div className="summary">
        <Tag color={getStatusColor()}>Status: {OrderStatus[order ? order!.state : 2]}</Tag>
        <div className="total">Total sale: {iskFormat(totalSale)}</div>
        <div className="total">Corp credit: {iskFormat(totalCredit)}</div>
        {buttonsToShow()}
    </div>;

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
                        <a href="/"> <img src={require('../../images/nilf_banner.png')} /></a>
                    </Col>
                    <Col>Repp's Buy Tool</Col>
                </Row>
            </Header>
            <Content>
                <Row gutter={16}>
                    <Col flex={2}>
                        {itemTable}
                    </Col>
                    <Col flex={3}>
                        <div className="cart">
                            <div className="user">
                                <span className="name">{order?.userNameDisplay}</span>
                                <img src={order?.userAvatarLink} />
                            </div>
                            {summary}
                        </div>
                    </Col>
                </Row>
            </Content>
        </Layout>
    )
}