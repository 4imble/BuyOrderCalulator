import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Row, Col,Layout, Table, Button } from 'antd';
import { Order } from '../../domain/domain';
import NumberFormat from 'react-number-format';

import './Orders.less'

const { Header, Content } = Layout;

export default function Orders(props: any) {
    const history = useHistory();
    const [allOrders, setAllOrders] = useState<Order[]>([]);

    useEffect(() => {
        if (props.user && !props.user.isAdmin)
            history.push("/");

        fetchOrders();
    }, [props.user]);

    async function fetchOrders() {
        const result = await fetch("/api/order");
        result.json().then(res => setAllOrders(res))
            .catch(err => console.log(err));
    }

    const iskFormat = (value: number) => <NumberFormat value={value} displayType={'text'} thousandSeparator={true} prefix={'Ƶ '} />

    function getTotalPrice(order: Order) {
        return order.orderItems.map(x => x.fixedUnitPrice * x.quantity).reduce((total, item) => total + item)
    }

    function redirectToOrder(guid: string)
    {
        history.push(`/order/${guid}`);
    }

    const itemColumns = [
        { title: 'Avatar', dataIndex: 'userAvatarLink', key: 'userAvatarLink', render: (value: string) => <img className="userimg" src={value} /> },
        { title: 'Name', dataIndex: 'userNameDisplay', key: 'userNameDisplay' },
        { title: 'Total', key: 'total', render: (cell: null, order: Order) => iskFormat(getTotalPrice(order)) },
        { dataIndex: 'guid', key: 'guid', render: (value: string) => <Button type="link" onClick={() => redirectToOrder(value)}>View</Button> },
    ]

    let itemTable = <Table rowKey='id' pagination={{ hideOnSinglePage: true }} className="table" columns={itemColumns} dataSource={allOrders} />;

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
                {itemTable}
            </Content>
        </Layout>
    )
}