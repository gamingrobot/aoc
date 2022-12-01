use std::{error::Error};

use array2d::Array2D;

#[aoc_generator(day4)]
fn gen(input: &str) -> Result<Game, Box<dyn Error>> {
    let mut iter = input.lines();
    let raw_numbers = iter.next().unwrap();
    let game = Game {
        numbers: raw_numbers.split(",").map(|x| x.parse().unwrap()).collect(),
        boards: Vec::new(),
    };
    
    let raw_boards: Vec<_> = iter.collect();
    let result = raw_boards.chunks(6).collect::<Vec<_>>().join(" ").split(" ");

    Ok(game)
}

#[aoc(day4, part1)]
pub fn part1(input: &Game) -> u32 {
    println!("{:?}", input);
1
}

// #[aoc(day4, part2)]
// pub fn part2(input: &Vec<Vec<u8>>) -> u32 {
// 1
// }

#[derive(Debug)]
pub struct Game {
    numbers: Vec<u32>,
    boards: Vec<BingoBoard>
}

#[derive(Debug)]
pub struct BingoBoard {
   board: Array2D<u32>
}