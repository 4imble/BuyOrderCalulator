import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Row, Col, Tag, Layout, Table, Button } from 'antd';
import { Order, OrderItem, OrderStatus } from '../../domain/domain';
import NumberFormat from 'react-number-format';
import roundTo from 'round-to';
import './Order.less'

const { Header, Content } = Layout;

export default function ViewOrder(props: any) {
    const history = useHistory();
    const [order, setOrder] = useState<Order>();

    useEffect(() => {
        fetchOrder();
    }, []);

    async function fetchOrder() {
        const result = await fetch(`/api/order/${props.match.params.id}`);
        result.json().then(res => setOrder(res))
            .catch(err => console.log(err));
    }

    function getTotalPrice(orderItem: OrderItem) {
        let value = orderItem.fixedUnitPrice * orderItem.quantity;
        return roundTo.up(value, 0);
    }

    function getCorpCredit(orderItem: OrderItem) {
        let value = Math.ceil((orderItem.fixedCorpCreditPercent / 100) * getTotalPrice(orderItem));
        return roundTo.up(value, 0);
    }

    const iskFormat = (value: number) => <NumberFormat value={roundTo(value, 2)} displayType={'text'} thousandSeparator={true} prefix={'Ƶ '} />

    async function submitOrder(state: OrderStatus) {
        await fetch("/api/order/processOrder", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ OrderGuid: order!.guid, State: state, DiscordId: props.user.discordId, AccessToken: props.user.accessToken })
        });

        await fetchOrder();
    }

    const auditorButton = () => {
        if (order?.isCancelled || !props.user?.isAuditor && !props.user?.isAdmin)
            return <></>;

        let text = order?.dateCredited ? "Set Not Credited" : "Set Credited";

        return <Button block type="default" onClick={() => submitOrder(OrderStatus.Credited)}>{text}</Button>
    };

    const acceptButton = () => {
        if (order?.isCancelled || !props.user?.isAdmin)
            return <></>;

        let text = order?.dateAccepted ? "Unset Accepted" : "Set Accepted";

        return <Button block type="primary" onClick={() => submitOrder(OrderStatus.Accepted)}>{text}</Button>
    };

    const cancelButton = () => {
        if (!props.user?.isAdmin)
            return <></>;

        let text = order?.isCancelled ? "Undo Cancel" : "Cancel Order";

        return <Button block type="primary" danger onClick={() => submitOrder(OrderStatus.Cancelled)}>{text}</Button>
    };

    let tags = order?.isCancelled ? <Tag color="red">Cancelled</Tag> :
        <>
            {order?.dateCreated ? <Tag>Created: {order!.dateCreated}</Tag> : <></>}
            {order?.dateCredited ? <Tag color="blue">Credited: {order!.dateCredited}</Tag> : <></>}
            {order?.dateAccepted ? <Tag color="green">Accepted: {order!.dateAccepted}</Tag> : <></>}
        </>

    let totalSale = order ? order!.orderItems.map(x => getTotalPrice(x)).reduce((total, item) => total + item) : 0;
    let totalCredit = order ? order!.orderItems.map(x => getCorpCredit(x)).reduce((total, item) => total + item) : 0;
    let summary = <div className="summary">
        {tags}
        <div className="total">Total sale: {iskFormat(totalSale)}</div>
        <div className="total">Corp credit: {iskFormat(totalCredit)}</div>
        {auditorButton()}
        {acceptButton()}
        {cancelButton()}
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
                <Row>
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