import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Card, Button, Row, Col } from 'antd';
import { Item } from '../../domain/domain';

export default function Lobby(props: any) {
    const history = useHistory();

    const [items, setItems] = useState<Item[]>([]);

    useEffect(() => {
        fetchItems();
    }, []);

    async function fetchItems() {
        const result = await fetch("api/items");
        result.json().then(res => setItems(res))
            .catch(err => console.log(err));
    }

    let itemRows = items.map((item, index) => <Row key={index}>
        <Col className="gutter-row" flex="auto"></Col>
        <Col className="gutter-row">
            {item.name}
        </Col>
        <Col className="gutter-row" flex="auto"></Col>
    </Row>)


    return (
        <div>{itemRows}</div>
    )
}