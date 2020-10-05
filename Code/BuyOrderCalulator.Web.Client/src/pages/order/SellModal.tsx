import React, { useState } from 'react';
import { Modal, Button, Input } from 'antd';
import { ShoppingCartOutlined } from '@ant-design/icons';
import NumberFormat from 'react-number-format';
import './Modal.less'

export default function SellModal(props: any) {
    const [visible, setVisible] = useState<boolean>(false);
    const [quantity, setQuantity] = useState<string>("");


    const showModal = () => {
        setVisible(true);
    };

    const handleOk = (e: any) => {
        props.addSaleItem({itemId: props.item.id, quantity});
        setVisible(false);
    };

    const handleCancel = (e: any) => {
        setQuantity("")
        setVisible(false);
    };

    const iskFormat = (value: number) => <NumberFormat value={value} displayType={'text'} thousandSeparator={true} prefix={'Ƶ '} />

    let title = `Selling ${props.item.name}`;

    return (
        <>
            <Button type="primary" onClick={showModal}>
                <ShoppingCartOutlined />
            </Button>
            <Modal className='modal' title={title} visible={visible} onOk={handleOk} onCancel={handleCancel}>
                <Input type="number" placeholder="Amount to sell" value={quantity} onChange={e => setQuantity(e.target.value)} />
                <div className='total'>Total: {iskFormat(Number(quantity) * props.item.unitPrice)}</div>
            </Modal>
        </>
    );
}