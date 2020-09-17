import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Card, Button, Row, Col, Input } from 'antd';
import { Item } from '../../domain/domain';

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

    let itemRows = filteredItems().map((item, index) => <Row key={index}>
        <Col className="gutter-row" flex="auto"></Col>
        <Col className="gutter-row">
            {item.name}
        </Col>
        <Col className="gutter-row" flex="auto"></Col>
    </Row>)


    return (
        <div>
            <Input placeholder="Name" value={search} onChange={e => setSearch(e.target.value)} />
            {itemRows}
        </div>
    )
}