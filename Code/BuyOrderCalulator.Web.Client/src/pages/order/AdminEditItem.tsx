import React, { useState } from 'react';
import { Modal, Button, Input, Select } from 'antd';
import './Modal.less'
import { ItemType, Item, SupplyType } from '../../domain/domain';

const { Option } = Select;

export default function AdminEditItem(props: any) {
    const [visible, setVisible] = useState<boolean>(false);

    function handleTypeChange(value: any) {
        props.item.typeId = value;
        saveItem();
    }

    function handleSupplyChange(value: any) {
        props.item.supplyTypeId = value;
        saveItem();
    }

    async function saveItem() {
        let item = props.item;
        await fetch("api/admin/saveItem", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ ItemId: item.id, ItemTypeId: item.typeId, SupplyTypeId: item.supplyTypeId, DiscordId: props.user.discordId, AccessToken: props.user.accessToken })
        });

        props.fetchItems();
    }

    
    const showModal = () => {
        setVisible(true);
    };

    const handleCancel = (e: any) => {
        setVisible(false);
    };

    let title = `Editing ${props.item.name}`;

    return (
        <>
            <Button type="link" onClick={showModal}>
                Edit
            </Button>
            <Modal className='modal' footer={null} title={title} visible={visible} onCancel={handleCancel}>
                <div className="editItem">
                    <Select defaultValue={props.item.typeId} style={{ width: 120 }} onChange={handleTypeChange}>
                        {props.commonData.itemTypes.map((itemType: ItemType) => <Option key={itemType.id} value={itemType.id}>{itemType.name}</Option>)}
                    </Select>
                    <Select defaultValue={props.item.supplyTypeId} style={{ width: 120 }} onChange={handleSupplyChange}>
                        {props.commonData.supplyTypes.map((supplyType: SupplyType) => <Option key={supplyType.id} value={supplyType.id}>{supplyType.name}</Option>)}
                    </Select>
                </div>
            </Modal>
        </>
    );
}