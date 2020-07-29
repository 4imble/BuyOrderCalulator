import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import TileElement from './TileElement';
import PlayerName from './PlayerName';
import { Tile, TileColor, ICoOrds } from './domain';
import { useCookies } from 'react-cookie';

export default function Game(props: any) {
  const history = useHistory();
  const [cookie, setCookie, removeCookie] = useCookies(['swarm']);
  const [tiles, setTiles] = useState<Array<Tile>>([new Tile(0, 0, TileColor.Green), new Tile(0, -2, TileColor.Orange), new Tile(1, -1, TileColor.Green)]);
  const [windowProps, setWindowProps] = useState({ width: 0, height: 0 });
  const [gameEdges, setGameEdges] = useState<Array<Tile>>([]);

  const calcEdges = () => {
    let allEdges: Array<ICoOrds> = [];
    tiles.forEach(tile => allEdges = allEdges.concat(tile.edges));
    let distinctEdges: Array<ICoOrds> = [...new Set(allEdges)];
    let unassignedDistinctEdges = distinctEdges.filter(edge => !tiles.some(tile => tile.x === edge.x && tile.y === edge.y));

    if (!unassignedDistinctEdges.length)
      unassignedDistinctEdges = [{ x: 0, y: 0 }];

    setGameEdges(unassignedDistinctEdges.map(edge => new Tile(edge.x, edge.y, TileColor.Unassigned)));
  }

  useEffect(() => {
    if (!cookie.swarm?.name)
      history.push("/");
  })

  useEffect(() => {
    updateWindowDimensions();
    window.addEventListener('resize', updateWindowDimensions);
    return () => window.removeEventListener('resize', updateWindowDimensions);
  }, []);

  useEffect(calcEdges, []);

  const updateWindowDimensions = () => {
    setWindowProps({ width: window.innerWidth, height: window.innerHeight });
  }

  let tileElements = tiles.map((tile, index) => <TileElement key={index} data={tile} window={windowProps}></TileElement>)
  let tileEdges = gameEdges.map((tile, index) => <TileElement key={index} data={tile} window={windowProps}></TileElement>)

  return (
    <div>
      {tileElements}
      {tileEdges}
      <PlayerName name={'Player 1'} color={'green'}></PlayerName>
      <PlayerName name={'Player 2'} color={'orange'} isActive={true}></PlayerName>
    </div>
  )
}