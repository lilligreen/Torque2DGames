//-----------------------------------------------------------------------------
// Copyright (c) 2013 Mike Lilligreen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

if (!isObject(FillLogicBehavior))
{
    %template = new BehaviorTemplate(FillLogicBehavior);
}

//-----------------------------------------------------------------------------

function FillLogicBehavior::floodFill(%this, %owner, %newColor)
{
    // Get the starting tile position based on the owner
    if (%owner == 0)
        %startPosition = "0 14";
    else
        %startPosition = "14 0";

    // Reset the score
    %this.owner.score[%owner] = 0;

    // Get the current tile color before we change it
    %this.owner.selectSprite(%startPosition);
    %oldColor = %this.owner.getSpriteImageFrame();

    // Create a stack object
    %stackObject = TamlRead("./../objects/Stack.taml");
    %locStack = %stackObject.getBehavior(StackBehavior);

    %locStack.push(%startPosition);

    while (%stackObject.len > 0)
    {
        // Pull the top card from the stack
        %topCard = %locStack.pop();

        %this.owner.selectSprite(%topCard);

        %dataObject = %this.owner.getSpriteDataObject();

        // If we have already visited this tile, skip it
        if (%dataObject.visited)
            continue;

        %dataObject.visited = true;

        // If this tile is owned by the opponent, skip it
        if (%dataObject.owner != %owner && %dataObject.owner != -1)
            continue;

        %color = %this.owner.getSpriteImageFrame();

        if (%color == %oldColor)
        {
            %dataObject.owner = %owner;
            %this.owner.setSpriteImageFrame(%newColor);
            %this.owner.deselectSprite();

            if (%topCard.x > 0)
            {
                %this.owner.selectSprite((%topCard.x - 1) SPC %topCard.y);
                %leftData = %this.owner.getSpriteDataObject();

                if (!%leftData.visited)
                    %locStack.push((%topCard.x - 1) SPC %topCard.y);
            }
            
            if (%topCard.x < 14)
            {
                %this.owner.selectSprite((%topCard.x + 1) SPC %topCard.y);
                %rightData = %this.owner.getSpriteDataObject();

                if (!%rightData.visited)
                    %locStack.push((%topCard.x + 1) SPC %topCard.y);
            }
            
            if (%topCard.y > 0)
            {
                %this.owner.selectSprite(%topCard.x SPC (%topCard.y - 1));
                %downData = %this.owner.getSpriteDataObject();

                if (!%downData.visited)
                    %locStack.push(%topCard.x SPC (%topCard.y - 1));
            }
            
            if (%topCard.y < 14)
            {
                %this.owner.selectSprite(%topCard.x SPC (%topCard.y + 1));
                %upData = %this.owner.getSpriteDataObject();

                if (!%upData.visited)
                    %locStack.push(%topCard.x SPC (%topCard.y + 1));
            }
        }
    }

    %this.owner.clearVisits();

    %locStack.push(%startPosition);

    while (%stackObject.len > 0)
    {
        %topCard = %locStack.pop();

        %this.owner.selectSprite(%topCard);

        %dataObject = %this.owner.getSpriteDataObject();

        if (%dataObject.visited)
            continue;

        %dataObject.visited = true;

        if (%dataObject.owner != %owner && %dataObject.owner != -1)
            continue;

        %color = %this.owner.getSpriteImageFrame();

        if (%color == %newColor)
        {
            %this.owner.score[%owner]++;

            %dataObject.owner = %owner;

            if (%topCard.x > 0)
            {
                %this.owner.selectSprite((%topCard.x - 1) SPC %topCard.y);
                %leftData = %this.owner.getSpriteDataObject();

                if (!%leftData.visited)
                    %locStack.push((%topCard.x - 1) SPC %topCard.y);
            }
            
            if (%topCard.x < 14)
            {
                %this.owner.selectSprite((%topCard.x + 1) SPC %topCard.y);
                %rightData = %this.owner.getSpriteDataObject();

                if (!%rightData.visited)
                    %locStack.push((%topCard.x + 1) SPC %topCard.y);
            }
            
            if (%topCard.y > 0)
            {
                %this.owner.selectSprite(%topCard.x SPC (%topCard.y - 1));
                %downData = %this.owner.getSpriteDataObject();

                if (!%downData.visited)
                    %locStack.push(%topCard.x SPC (%topCard.y - 1));
            }
            
            if (%topCard.y < 14)
            {
                %this.owner.selectSprite(%topCard.x SPC (%topCard.y + 1));
                %upData = %this.owner.getSpriteDataObject();

                if (!%upData.visited)
                    %locStack.push(%topCard.x SPC (%topCard.y + 1));
            }
        }
    }

    %this.owner.clearVisits();
    %stackObject.delete();
}

function FillLogicBehavior::clearVisits(%this)
{
    // Get the number of sprites in the composite
    %count = %this.owner.getSpriteCount();
    %length = mSqrt(%count);

    // Iterate over all the sprites
    for (%x = 0; %x < %length; %x++)
    {
        for (%y = 0; %y < %length; %y++)
        {
            // Select the tile
            %this.owner.selectSprite(%x SPC %y);

            // Get the data object
            %dataObject = %this.owner.getSpriteDataObject();

            // Clear
            %dataObject.visited = false;
        }
    }
}

function FillLogicBehavior::bestMove(%this)
{
    // This strategy will make the computer tough to beat
    %color = getRandom(0,5);
    
    %this.owner.floodFill(1, %color);
}