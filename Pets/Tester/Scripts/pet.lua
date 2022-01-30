windows = GetWindows(false, true)
baseGravity = 0.53
desktopHeight = desktopBounds.h
--gravityMultiplier = 0.5
yVelocity = 0
yMaxVelocity = 30
jump = false
grounded = false
col = RBG(255, 0, 0)

function _Start()
    pet.animation = "idle"
    pet.x = desktopBounds.w/2
    pet.y = desktopHeight
    show = true
end

function _Update()
    -- take input
    jump = IsKeyDown(virtualKey.SPACE)
    
    if IsKeyHeld(virtualKey.D) then
        pet.animation = "run"
        pet.flipX = false
        pet.x = pet.x + 8
    elseif IsKeyHeld(virtualKey.A) then
        pet.animation = "run"
        pet.flipX = true
        pet.x = pet.x - 8
    else
        pet.animation = "idle"
    end
    
    -- print(jump)

    if jump or not grounded then
        pet.animation = "jump"
        
        if yVelocity > 3.5 then
            pet.animation = "fall"
        end
    end
end

function _LateUpdate()
    windows = GetWindows(true, true)

    yVelocity = yVelocity + baseGravity
    grounded = false

    if (yVelocity > yMaxVelocity) then 
        yVelocity = yMaxVelocity
    end

    -- check ground collision
    if pet.y >= desktopHeight then
        pet.y = desktopHeight
        yVelocity = 0
        grounded = true
    end

    -- check window collisions
    for k, v in pairs(windows) do
        pb = AABBFromXYWH(pet.AABB.x + 72, pet.y - 20 + yVelocity, pet.AABB.w - 128, 20)
        wb = AABBFromXYWH(v.AABB.x, v.AABB.y, v.AABB.w, 20)
        
        if IsCollidingAABB(pb, wb) and yVelocity >= 0 then
            pet.y = wb.y + 1
            yVelocity = 0
            grounded = true
        end
    end

    if grounded and jump then
        yVelocity = -15
        grounded = false
    end

    pet.y = pet.y + yVelocity
end

function _Draw()
    -- pb = AABBFromXYWH(pet.AABB.x + 72, pet.y - 20 + yVelocity, pet.AABB.w - 128, 20)
    -- windows = GetWindows(true, true)
    -- _DrawRect(pb, col)

    -- for k, v in pairs(windows) do
    --     _DrawRect(v.AABB, col)
    -- end
end

-- function 