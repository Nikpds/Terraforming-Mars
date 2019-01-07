import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  games = [
    {
      title: 'Terraforming Mars (2016)',
      creator: '@Jacob Fryxelius',
      players: '1-5',
      time: '120 min',
      age: '12+',
      image: 'terraforming-base.jpg',
      text: `Corporations are competing to transform Mars into a habitable planet by spending vast+
      resources, and using innovative technology to raise temperature, create a breathable
      atmosphere, and make oceans of water.As terraforming progresses, more and more people
      will immigrate from Earth to live on the Red Planet.
      In Terraforming Mars, you control a corporation with a certain profile.Play project
      cards, build up production, place your cities and green areas on the map, and race for
      milestones and awards!`
    },
    {
      title: 'Terraforming Mars: Hellas & Elysium (2017)',
      creator: '@Jacob Fryxelius',
      players: '1-5',
      time: '120 min',
      age: '12+',
      image: 'elysium.jpg',
      text: ` Terraforming Mars: Hellas & Elysium, the first expansion for Terraforming Mars,
      consists of a double-sided game board presenting two new areas of Mars.
      Elysium takes players almost to the opposite side of Mars' equator, with vast
      lowlands for oceans in the north and a dry, mineral-rich south.
      Hellas, the southern wild, includes Mars' south pole and the enormous seven-hex
      Hellas crater that just begs to become a giant lake.`
    },
    {
      title: 'Terraforming Mars: Venus Next (2017)',
      creator: '@Jacob Fryxelius',
      players: '1-5',
      time: '120 min',
      age: '12+',
      image: 'venus.jpg',
      text: `Terraforming Mars: Venus Next, the second expansion for the Terraforming Mars base
      game, has players building flying cities and making the atmosphere more hospitable on
      the deadly planet Venus.
      Around 50 project cards and 5 corporations are added. With the new floater resource,
      a new milestone, a new award, a
      new tag, and a new terraforming parameter, players are given more paths to victory and
      an even more varied play.`
    },
    {
      title: 'Terraforming Mars: Prelude (2018)',
      creator: '@Jacob Fryxelius',
      players: '1-5',
      time: '120 min',
      age: '12+',
      image: 'prelude.jpg',
      text: `As the mega corporations are getting ready to start the terraforming process, you now
      have the chance to make those early choices that will come to define your corporation
      and set the course for the future history of Mars - this is the prelude to your
      greatest endeavors!
      In Terraforming Mars: Prelude, you get to choose from Prelude cards that jumpstart the
      terraforming process, or boost your corporation engine. There are also 5 new
      corporations, and 7 project cards that thematically fit the early stages of
      terraforming.`
    },
    {
      title: 'Terraforming Mars: Colonies (2018)',
      creator: '@Jacob Fryxelius',
      players: '1-5',
      time: '120 min',
      age: '12+',
      image: 'colonies.jpg',
      text: ` Our world has widened before us. Corporations expand their operations to all corners of
      the solar system in pursuit of minerals and resources. Most places are not suited for
      terraforming, but building colonies may greatly improve your income and your
      possibilities in achieving that higher goal — the terraforming of Mars. Send your trade
      fleet to distant moons! Colonize the clouds of Jupiter! And let your Earth assets
      propel you to success!`
    }
  ];

  constructor() { }

  ngOnInit() {
  }


}
